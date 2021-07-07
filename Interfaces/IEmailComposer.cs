using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailComposer
    {
        public string ComposeConfirmEmail(string name, string confirmUrl);
        public string ComposeResetPassword(string name, string confirmUrl);
        public string ComposeBookingConfirmation(Booking booking);
        public string ComposeBookingCancelation(Booking booking);
        public string ComposeTripPromotion(Trip trip, string detailUrl, string bookingUrl);

    }
}
