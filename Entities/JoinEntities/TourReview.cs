using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class TourReview : OwnedTrackedEntityWithKey<Customer, int, string>
    {
        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Content { get; set; }
        [Range(1, 5)]
        public int Stars { get; set; }
        public int TourID { get; set; }
        public Tour Tour { get; set; }
    }
}
