using Core.Interfaces;

namespace Core.Entities
{
    public class Itinerary: EntityWithKey<int>
    {
        public string Title { get; set; }
        public string Accommodation { get; set; }
        public string Meal { get; set; }
        public string Transport { get; set; }
        public string Activity { get; set; }
        public int TripID { get; set; }
    }
}
