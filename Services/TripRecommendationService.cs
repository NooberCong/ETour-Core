using Core.Entities;
using FuzzySharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class TripRecommendationService
    {
        public IEnumerable<Trip> GetRecommendations(IEnumerable<Trip> source, Trip baseTrip)
        {
            return source.Where(tr => tr.CanBook(DateTime.Now) && tr.ID != baseTrip.ID)
                .OrderByDescending(tr => GetSimilarPoints(tr, baseTrip))
                .Take(5);
        }

        public double GetSimilarPoints(Trip source, Trip other)
        {
            double points = 0;

            // Same tour type
            if (source.Tour.Type == other.Tour.Type)
            {
                points += 75;
            }

            // Same start place and/or destination
            if (Fuzz.PartialRatio(source.Tour.StartPlace, other.Tour.StartPlace) >= 85)
            {
                points += 50;
            }
            if (Fuzz.PartialRatio(source.Tour.Destination, other.Tour.Destination) >= 85)
            {
                points += 50;
            }

            // Similar Price
            points -= Convert.ToDouble(Math.Abs(source.GetSalePrice() - other.GetSalePrice()));

            // Similar Start Time and End Time
            points += 25 - (source.StartTime - other.StartTime).TotalDays;
            points += 25 - (source.EndTime - other.EndTime).TotalDays;

            // Similar Description
            points += Fuzz.TokenSetRatio(source.Tour.Description, other.Tour.Description) * .25;

            return points;
        }
    }
}
