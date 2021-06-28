using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core.Entities
{
    public class Booking : OwnedTrackedEntityWithKey<Customer, int, string>
    {
        public static readonly decimal _maxPointRatio = .8m;
        public Trip Trip { get; set; }
        public int TripID { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal Total { get; set; }

        public BookingStatus Status { get; set; }

        [Required]
        [Display(Name = "Most Valued Quality")]
        public BookingMostValued MostValued { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateDeposited { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCompleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PaymentDeadline { get; set; }

        public ICollection<CustomerInfo> CustomerInfos { get; set; } = new List<CustomerInfo>();

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(128, MinimumLength = 3)]
        public string ContactName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(128, MinimumLength = 3)]
        [Display(Name = "Email Address")]
        public string ContactEmail { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [Display(Name = "Home Address")]
        [StringLength(256, MinimumLength = 3)]
        public string ContactAddress { get; set; }

        [Range(1, int.MaxValue)]
        public int TicketCount { get; set; }

        public int? PointsApplied { get; set; }
        public int? Refunded { get; set; }

        public decimal? Deposit { get; set; }

        public bool Reviewed { get; set; }

        [NotMapped]

        public List<PointLog> PointLogs { get; set; } = new();

        public int GetApplicablePoints(int points)
        {
            return Convert.ToInt32(Math.Floor(Math.Min(points, Total * _maxPointRatio)));
        }

        public decimal GetFinalPayment()
        {
            return Total - Deposit.Value;
        }

        public IEnumerable<BookingStatus> GetPossibleNextStatuses()
        {
            return Status switch
            {
                BookingStatus.Awaiting_Deposit => new BookingStatus[] { BookingStatus.Processing, BookingStatus.Awaiting_Payment, BookingStatus.Canceled },
                BookingStatus.Processing => new BookingStatus[] { BookingStatus.Awaiting_Payment, BookingStatus.Canceled },
                BookingStatus.Awaiting_Payment => new BookingStatus[] { BookingStatus.Completed, BookingStatus.Canceled },
                BookingStatus.Completed => new BookingStatus[] { BookingStatus.Canceled },
                BookingStatus.Canceled => Array.Empty<BookingStatus>(),
                _ => throw new InvalidOperationException(),
            };
        }

        public Invoice GenerateDepositInvoice(Invoice.PaymentMethod method)
        {
            return new Invoice { 
                Amount = Deposit.Value,
                BookingID = ID,
                LastUpdated = DateDeposited.Value,
                Method = method,
                Type = Invoice.PaymentType.Deposit,
                Note = $"(Generated) {Owner.Name} pays Deposit for Booking No.{ID} {Trip.Tour.Title}"
            };
        }

        public Invoice GenerateFinalPaymentInvoice(Invoice.PaymentMethod method)
        {
            return new Invoice
            {
                Amount = GetFinalPayment(),
                BookingID = ID,
                LastUpdated = DateCompleted.Value,
                Method = method,
                Type = Invoice.PaymentType.Full_Payment,
                Note = $"(Generated) {Owner.Name} pays Final Payment for Booking No.{ID} {Trip.Tour.Title}"
            };
        }

        public void ChangeStatus(BookingStatus newStatus)
        {
            if (!GetPossibleNextStatuses().Contains(newStatus))
            {
                throw new InvalidOperationException("Invalid booking status change");
            }
            if (Status == BookingStatus.Awaiting_Deposit)
            {
                DateDeposited = DateTime.Today;
                PaymentDeadline = Trip.StartTime.AddDays(-5);
            }
            else if (Status == BookingStatus.Awaiting_Payment)
            {
                DateCompleted = DateTime.Today;
            }
            Status = newStatus;
        }

        public BookingCancelInfo GetBookingCancelInfo(DateTime cancelDate)
        {
            decimal amountPaid = 0;
            // Customer has paid the full amount
            if (DateCompleted.HasValue)
            {
                amountPaid = Total;
            }
            // Customer has not paid full but paid deposit
            else if (DateDeposited.HasValue)
            {
                amountPaid = Deposit.Value;
            }
            var daysEarly = (Trip.StartTime - cancelDate).TotalDays;
            var ratioLost = CalculateCancelRatio(daysEarly);
            var refund = Convert.ToInt32(Math.Max(0, Math.Ceiling(amountPaid - Total * ratioLost)));

            return new BookingCancelInfo
            {
                BookingID = ID,
                AmountLost = amountPaid - refund,
                Refund = refund,
                PointsLost = PointsApplied.Value,
                DaysEarly = Convert.ToInt32(daysEarly),
                Trip = Trip
            };
        }


        private static decimal CalculateCancelRatio(double daysEarly)
        {
            if (daysEarly >= 20)
            {
                return .3m;
            } else if (daysEarly >= 15)
            {
                return .5m;
            } else if (daysEarly >= 10)
            {
                return .7m;
            } else if (daysEarly >= 5)
            {
                return .9m;
            }

            return 1;
        }

        public void SetDeposit(float depositPercentage)
        {
            Deposit = Total / 100 * (decimal)depositPercentage;
        }

        public int GetMemberCountByAgeGroup(CustomerInfo.CustomerAgeGroup ageGroup)
        {
            return CustomerInfos.Where(ci => ci.AgeGroup == ageGroup).Count();
        }

        public bool CanCancel(DateTime dateCancel)
        {
            return Status != BookingStatus.Canceled && Trip.StartTime >= dateCancel;
        }

        public void Cancel(DateTime dateCancel)
        {
            if (!CanCancel(dateCancel))
            {
                throw new InvalidOperationException("Attempting to cancel a uncancellable booking");
            }

            var cancelInfo = GetBookingCancelInfo(dateCancel);

            Refunded = cancelInfo.Refund;
            ChangeStatus(BookingStatus.Canceled);
        }

        public bool CanBeReviewed(DateTime dateReview)
        {
            return Status == Booking.BookingStatus.Completed && !CanCancel(dateReview) && !Reviewed;
        }

        public void ChargePoints(Customer customer)
        {
            if (PointsApplied.HasValue)
            {
                customer.Points -= PointsApplied.Value;
                PointLogs.Add(new PointLog { 
                    OwnerID = customer.ID,
                    LastUpdated = DateTime.Now,
                    Amount = PointsApplied.Value,
                    Trigger = $"Booking No.{ID}",
                    Description = $"{PointsApplied.Value} points used to book"
                });
            }
        }

        public void RewardPoints(Customer customer)
        {
            if (Trip.RewardPoints > 0)
            {
                customer.Points += Trip.RewardPoints;
                PointLogs.Add(new PointLog
                {
                    OwnerID = customer.ID,
                    LastUpdated = DateTime.Now,
                    Amount = Trip.RewardPoints,
                    Trigger = $"Booking No.{ID}",
                    Description = $"{Trip.RewardPoints} points rewarded on successful booking"
                });
            }
        }

        public void RefundPoints(Customer customer)
        {
            if (Refunded.HasValue)
            {
                customer.Points += Refunded.Value;
                PointLogs.Add(new PointLog
                {
                    OwnerID = customer.ID,
                    LastUpdated = DateTime.Now,
                    Amount = Refunded.Value,
                    Trigger = $"Booking No.{ID}",
                    Description = $"{PointsApplied.Value} points refunded on cancelation"
                });
            }
        }

        public enum BookingStatus
        {
            Awaiting_Deposit,
            Processing,
            Awaiting_Payment,
            Completed,
            Canceled,
        }

        public enum BookingMostValued
        {
            Transportation,
            Accomodation,
            Activities,
            Cuisine
        }

        public class BookingCancelInfo
        {
            public int BookingID { get; set; }
            public int Refund { get; set; }
            public int PointsLost { get; set; }
            public decimal AmountLost { get; set; }
            public int DaysEarly { get; set; }
            public Trip Trip { get; set; }
        }
    }
}
