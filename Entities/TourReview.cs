using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class TourReview : TrackedEntityWithKey<int>
    {
        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Content { get; set; }
        [Range(1, 5)]
        public int Stars { get; set; }
        public Booking Booking { get; set; }
        public int BookingID { get; set; }
    }
}
