using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Invoice : TrackedEntityWithKey<int>
    {
        [Range(0, double.MaxValue)]
        public Booking Booking { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }
        public PaymentMethod Method { get; set; }
        public string Note { get; set; }


        public enum PaymentType
        {
            Deposit,
            Full_Payment,
            Refund
        }

        public enum PaymentMethod
        {
            Cash,
            Bank_Transfer,
            Zalo_Pay,
            Momo,
            GPay
        }
    }
}
