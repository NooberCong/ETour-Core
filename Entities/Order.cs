using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Order : EntityWithKey<int>
    {
        public Customer Customer { get; set; }
        public string CustomerID { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime DateOrdered { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
