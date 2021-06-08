using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Booking : AuthoredTrackedEntityWithKey<Customer, int, string>
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }
        public int OrderID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal? Total { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal? Deposited { get; set; }

        public BookingStatus Status { get; set; }

        public BookingPaymentType? DepositPaymentType { get; set; }

        public BookingPaymentType? FullPaymentType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateDeposited { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCompleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PaymentDeadline { get; set; }

        public ICollection<CustomerInfo> CustomerInfos { get; set; }

        public enum BookingStatus
        {
            AwaitingDeposit,
            Processing,
            AwaitingPayment,
            Completed
        }

        public enum BookingPaymentType
        {
            Cash,
            Credit_Card
        }
    }
}
