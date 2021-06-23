using Core.Entities;
using Core.Interfaces;
using FuzzySharp;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class BlogRecommendationService
    {
        public IEnumerable<IPost<T>> GetRecommendations<T>(IEnumerable<IPost<T>> source, IPost<T> basePost) where T : IEmployee
        {
            return source.Where(p => !p.IsSoftDeleted && p.ID != basePost.ID).OrderByDescending(p => GetSimilarPoints(p, basePost)).Take(5);
        }

        public double GetSimilarPoints<T>(IPost<T> source, IPost<T> other) where T: IEmployee
        {
            double points = 0;

            // Same author
            if (source.AuthorID == other.AuthorID)
            {
                points += 50;
            }

            // Same category
            if (source.Category == other.Category)
            {
                points += 50;
            }

            // Similar title
            points += Fuzz.PartialTokenSetRatio(source.Title, other.Title);

            // Similar tags
            points += Fuzz.PartialTokenSetRatio(string.Join(" ", source.Tags), string.Join(" ", other.Tags));

            // Close date of publish
            points -= (source.LastUpdated - other.LastUpdated).TotalDays;

            return points;
        }

    }
}