using Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Tour : EntityWithKey<int>
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Start place")]
        [StringLength(256, MinimumLength = 5)]
        public string StartPlace { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Destination { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 5)]
        public string Description { get; set; }
        [NotMapped]
        public TourReviewSummary ReviewSummary { get; set; }
        public bool IsOpen { get; set; } = true;
        [Required]
        public TourType Type { get; set; } = TourType.Domestic;
        public List<string> ImageUrls { get; set; } = new List<string>();
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
        public ICollection<TourFollowing> Followings { get; set; }

        public enum TourType
        {
            Domestic,
            International
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            foreach (var trip in Trips)
            {
                trip.IsOpen = false;
            }
        }
    }
}
