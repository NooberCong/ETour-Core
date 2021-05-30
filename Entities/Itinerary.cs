using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Itinerary : EntityWithKey<int>
    {
        public Trip Trip { get; set; }
        public int TripID { get; set; }

        [Required]
        [Display(Name = "Starting time")]
        public DateTime StartTime { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Accommodation { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Meal { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Transport { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Activity { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 15, ErrorMessage = "Field detail must have a minimum length of 15")]
        public string Detail { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
