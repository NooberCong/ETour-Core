using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Entities
{
    public class Customer : SoftDeleteEntityWithKey<string>
    {
        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string ImgUrl { get; set; }
        public int Points { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime? LastSignIn { get; set; }
        public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<TourFollowing> TourFollowings { get; set; }
        public ICollection<PointLog> PointLogs { get; set; }

        public void Reward(int points)
        {
            Points += points;
        }

        public void Consume(int points)
        {
            Points -= points;
        }

        public bool IsFollowing(Tour tour)
        {
            return TourFollowings.Any(tf => tf.TourID == tour.ID);
        }
    }
}
