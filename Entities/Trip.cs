using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Trip : EntityWithKey<int>
    {
        private static readonly decimal _minPriceRatio = 0.9M;

        [Required]
        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100)]
        public int Capacity { get; set; }
        public int TourID { get; set; }
        public Tour Tour { get; set; }
        public bool IsOpen { get; set; } = true;
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
        public ICollection<TripDiscount> TripDiscounts { get; set; } = new List<TripDiscount>();

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
            decimal discounted = Price;
            foreach (var tripDisc in TripDiscounts)
            {
                discounted = tripDisc.Discount.Apply(discounted);
            }
            decimal minimumSalePrice = Price * _minPriceRatio;

            return Math.Max(discounted, minimumSalePrice);
        }
    }
}
