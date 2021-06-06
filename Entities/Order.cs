using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Order : EntityWithKey<int>
    {
        public Customer Customer { get; set; }
        public string CustomerID { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal? Total { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal? Deposited { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public List<Booking> Bookings { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        public enum OrderStatus
        {
            Pending,
            Processing,
            Awaiting_Payment,
            Expired,
            Completed,
            Canceled
        }
    }
}
