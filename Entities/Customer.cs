using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Customer : SoftDeleteEntityWithKey<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImgUrl { get; set; }
        public int Points { get; set; }
        public DateTime LastSignIn { get; set; }
        public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
        public ICollection<Order> Orders { get; set; }
        public ICollection<TourFollowing> TourFollowings { get; set; }
    }
}
