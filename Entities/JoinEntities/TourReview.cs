using Core.Interfaces;

namespace Core.Entities
{
    public class TourReview : AuthoredTrackedEntityWithKey<Customer, int, string>
    {
        public string Content { get; set; }
        public int Stars { get; set; }
        public int TourID { get; set; }
        public Tour Tour { get; set; }
    }
}
