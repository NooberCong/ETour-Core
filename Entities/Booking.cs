using Core.Interfaces;

namespace Core.Entities
{
    public class Booking : TrackedEntityWithKey<int>
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public BookingStatus Status { get; set; }


        public enum BookingStatus
        {
            Pending,
            Active,
            Expired,
            Canceled,
        }

        //public decimal CalculatePrice()
        //{
        //    return 0;
        //}

        //public decimal CalculateCancelationPrice()
        //{
        //    return 0;
        //}

        public void Cancel()
        {
            Status = BookingStatus.Canceled;
        }

    }
}
