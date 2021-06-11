using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Discount : EntityWithKey<int>
    {
        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Valid from")]
        [DataType(DataType.DateTime)]
        public DateTime ValidFrom { get; set; }

        [Required]
        [Display(Name = "Valid until")]
        [DataType(DataType.DateTime)]
        public DateTime ValidUntil { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }
        public DiscountType Type { get; set; } = DiscountType.Percentage;
        public ICollection<TripDiscount> TripDiscounts { get; set; } = new List<TripDiscount>();


        public enum DiscountType
        {
            Percentage,
            Amount
        }

        public string GetValueSuffix()
        {
            return Type switch
            {
                DiscountType.Percentage => "%",
                DiscountType.Amount => "$",
                _ => throw new NotImplementedException(),
            };
        }

        public decimal Apply(decimal price)
        {
            if (IsExpired(DateTime.Now))
            {
                throw new Exception("Attempting to apply expired discount");
            }
            return Type switch
            {
                DiscountType.Percentage => price / 100 * (100 - Value),
                DiscountType.Amount => price - Value,
                _ => throw new NotImplementedException(),
            };
        }

        public bool IsExpired(DateTime currentTime)
        {
            return ValidUntil.CompareTo(currentTime) <= 0;
        }
    }
}
