using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BookingStatusSegmentation
    {
        public int[] Segments { get; set; } = new int[Enum.GetValues(typeof(Booking.BookingStatus)).Length];

        public void SetSegment(Booking.BookingStatus status, int noBookings)
        {
            Segments[(int)status] = noBookings;
        }
    }
}
