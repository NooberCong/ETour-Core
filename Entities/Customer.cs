using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.DateTime)]
        public DateTime? DOB { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime? LastSignIn { get; set; }
        public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<TourFollowing> TourFollowings { get; set; }
        public ICollection<PointLog> PointLogs { get; set; }
    }
}
