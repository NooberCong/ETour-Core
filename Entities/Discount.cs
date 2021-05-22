using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Discount : EntityWithKey<int>
    {
        public string Title { get; set; }
        public string ValidUntil { get; set; }
        public decimal Value { get; set; }
        public DiscountType Type { get; set; } = DiscountType.Percentage;
        public ICollection<Trip> TripsApplied { get; set; } = new List<Trip>();


        public enum DiscountType
        {
            Percentage,
            Amount
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
