using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Customer : DeleteEntityWithKey<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImgUrl { get; set; }
        //public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
    }
}
