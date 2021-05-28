using Core.Interfaces;

namespace Core.Entities
{
    public class TripDiscount
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }
        public Discount Discount { get; set; }
        public int DiscountID { get; set; }
    }
}
