using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Trip: EntityWithKey<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public int TourID { get; set; }
        public Tour Tour { get; set; }
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();

        public bool IsVisible(DateTime currentTime)
        {
            return Tour.IsOpen && EndTime.CompareTo(currentTime) > 0;
        }
    }
}
