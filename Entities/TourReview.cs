using Core.Interfaces;

namespace Core.Entities
{
    public class TourReview: AuthoredTrackedDeleteEntityWithKey<Customer, int>
    {
        public string Content { get; set; }
        public int Stars { get; set; }

        public int TourID { get; set; }
        public string AuthorID { get; set; }
    }
}
