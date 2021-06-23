using Core.Entities;
using Core.Value_Objects;
using FuzzySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Core.Services
{
    public class TripFilterService
    {
        public IEnumerable<Trip> ApplyFilter(IEnumerable<Trip> trips, TripFilterParams filterParams)
        {
            return trips.Where(trip => trip.CanBook(DateTime.Now)
            && (filterParams.TourID == null || trip.TourID == filterParams.TourID)
            && (string.IsNullOrWhiteSpace(filterParams.Keyword) || Fuzz.PartialTokenSetRatio(trip.Tour.Title, filterParams.Keyword) >= 50 || Fuzz.PartialTokenSetRatio(trip.Tour.Description, filterParams.Keyword) >= 50)
            && (filterParams.TourType == null || trip.Tour.Type == filterParams.TourType)
            && (filterParams.Starts == null || trip.StartTime.Date == filterParams.Starts.Value.Date)
            && (filterParams.Ends == null || trip.EndTime.Date == filterParams.Ends.Value.Date)
            && (filterParams.MinPrice == null || trip.GetSalePrice() >= filterParams.MinPrice)
            && (filterParams.MaxPrice == null || trip.GetSalePrice() <= filterParams.MaxPrice))
                .OrderBy(trip => string.IsNullOrEmpty(filterParams.Keyword) ? 0: -Fuzz.PartialTokenSetRatio(trip.Tour.Title, filterParams.Keyword) * 1.5 - Fuzz.PartialTokenSetRatio(trip.Tour.Description, filterParams.Keyword));
        }
    }
}
