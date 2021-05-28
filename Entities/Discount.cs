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
            switch (Type)
            {
                case DiscountType.Percentage:
                    return "%";
                case DiscountType.Amount:
                    return "$";
                default:
                    throw new NotImplementedException();
            }
        }

        public decimal Apply(decimal price)
        {
            if (!IsValid(DateTime.Now))
            {
                throw new Exception("Attempting to apply invalid discount");
            }
            switch (Type)
            {
                case DiscountType.Percentage:
                    return price / 100 * Value;
                case DiscountType.Amount:
                    return price - Value;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool IsValid(DateTime currentTime)
        {
            // Invalid discount value
            if (Value <= 0 || Type == DiscountType.Percentage && Value > 100)
            {
                return false;
            }
            // Discount ended
            if (ValidUntil.CompareTo(currentTime) < 0)
            {
                return false;
            }
            return true;
        }
    }
}
