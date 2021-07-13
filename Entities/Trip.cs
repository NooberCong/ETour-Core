using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core.Entities
{
    public class Trip : EntityWithKey<int>
    {
        private static readonly decimal _minPriceRatio = 0.8M;

        [Required]
        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The given price is not realistic")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100)]
        public int Capacity { get; set; }

        [Required]
        [Range(20, 80)]
        public float Deposit { get; set; }
        public int TourID { get; set; }
        public Tour Tour { get; set; }
        public bool IsOpen { get; set; } = true;
        [Required]
        [Display(Name = "Reward Points")]
        [Range(0, 50)]
        public int RewardPoints { get; set; }

        [NotMapped]
        public int Vacancies
        {
            get
            {
                var bookingsCount = Bookings
                    .Where(bk => bk.Status != Booking.BookingStatus.Canceled)
                    .Select(bk => bk.TicketCount).Sum();

                return Capacity - bookingsCount;
            }
        }

        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
        public ICollection<TripDiscount> TripDiscounts { get; set; } = new List<TripDiscount>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public string OwnerID { get; set; }

        public bool IsVisible(DateTime currentTime)
        {
            return Tour.IsOpen && EndTime.CompareTo(currentTime) > 0;
        }

        public int GetDuration()
        {
            return EndTime.Subtract(StartTime).Days;
        }

        public decimal GetSalePrice()
        {
            decimal salePrice = Price;
            foreach (var discount in TripDiscounts.Select(trd => trd.Discount).Where(d => !d.IsExpired(DateTime.Now)))
            {
                salePrice = discount.Apply(salePrice);
            }
            decimal minimumSalePrice = Price * _minPriceRatio;

            return Math.Max(salePrice, minimumSalePrice);
        }

        public bool CanOpen()
        {
            return Tour.IsOpen;
        }

        public bool CanBook(DateTime bookTime)
        {
            return IsOpen && StartTime.AddDays(-30) > bookTime && Vacancies > 0;
        }

        public void Open()
        {
            if (!CanOpen())
            {
                throw new InvalidOperationException("Attempted to open a trip that belong to a closed tour");
            }
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

        public decimal GetSalePriceFor(CustomerInfo.CustomerAgeGroup ageGroup)
        {
            var ratio = ageGroup switch
            {
                CustomerInfo.CustomerAgeGroup.Adult => 1,
                CustomerInfo.CustomerAgeGroup.Youth => .5,
                CustomerInfo.CustomerAgeGroup.Children => .3,
                CustomerInfo.CustomerAgeGroup.Baby => 0,
                _ => throw new InvalidOperationException("No ratio known for age group"),
            };

            return GetSalePrice() * (decimal)ratio;
        }

        public decimal[] GetSalePricesFor(IEnumerable<CustomerInfo.CustomerAgeGroup> ageGroups)
        {
            int freeBabies = ageGroups.Where(ag => ag == CustomerInfo.CustomerAgeGroup.Adult).Count();
            var salePrices = new List<decimal>();

            foreach (var ag in ageGroups)
            {
                if (ag == CustomerInfo.CustomerAgeGroup.Baby)
                {
                    if (freeBabies > 0)
                    {
                        freeBabies -= 1;
                        salePrices.Add(GetSalePriceFor(CustomerInfo.CustomerAgeGroup.Baby));
                    }
                    else
                    {
                        salePrices.Add(GetSalePriceFor(CustomerInfo.CustomerAgeGroup.Children));
                    }
                }
                else
                {
                    salePrices.Add(GetSalePriceFor(ag));
                }
            }

            return salePrices.ToArray();
        }
    }
}

