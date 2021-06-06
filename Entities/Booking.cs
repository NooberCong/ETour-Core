using Core.Interfaces;

namespace Core.Entities
{
    public class Booking : TrackedEntityWithKey<int>
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
