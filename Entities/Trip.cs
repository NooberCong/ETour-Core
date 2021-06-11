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
        [Range(0, 60)]
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
                var bookingsCount = this.Bookings
                    .Where(bk => bk.Status != Booking.BookingStatus.Canceled)
                    .Select(bk => bk.Amount).Sum();

                return Capacity - bookingsCount;
            }
        }

        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
        public ICollection<TripDiscount> TripDiscounts { get; set; } = new List<TripDiscount>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

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
    }
}
