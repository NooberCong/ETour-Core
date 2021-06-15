using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Booking : AuthoredTrackedEntityWithKey<Customer, int, string>
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unrealistic monetary value")]
        public decimal Total { get; set; }

        public BookingStatus Status { get; set; }

        [Required]
        [Display(Name = "Most Valued Quality")]
        public BookingMostValued MostValued { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateDeposited { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCompleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PaymentDeadline { get; set; }

        public ICollection<CustomerInfo> CustomerInfos { get; set; } = new List<CustomerInfo>();

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(128, MinimumLength = 3)]
        public string ContactName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(128, MinimumLength = 3)]
        [Display(Name = "Email Address")]
        public string ContactEmail { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [Display(Name = "Home Address")]
        [StringLength(256, MinimumLength = 3)]
        public string ContactAddress { get; set; }

        [NotMapped]
        public int Amount => CustomerInfos.Count;


        public decimal GetDeposit()
        {
            return Total * (decimal)Trip.Deposit / 100;
        }

        public enum BookingStatus
        {
            AwaitingDeposit,
            Processing,
            AwaitingPayment,
            Completed,
            Canceled,
        }

        public enum BookingMostValued
        {
            Transportation,
            Accomodation,
            Activities,
            Cuisine
        }

        public enum BookingPaymentProvider
        {
            Zalo_Pay,
            MoMo,
            Google_Pay
        }
    }
}
