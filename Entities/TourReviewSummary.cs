using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class TourReviewSummary
    {
        public double AverageStars { get; set; }
        public int NumberOfReviews { get; set; }

        public static TourReviewSummary FromReviews(IEnumerable<TourReview> reviews)
        {
            return new TourReviewSummary { 
                AverageStars = reviews.Any()? reviews.Sum(rev => rev.Stars) / reviews.Count(): 0,
                NumberOfReviews = reviews.Count()
            };
        }
    }
}
