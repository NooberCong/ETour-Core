using Core.Entities;
using Core.Value_Objects;
using System;
using System.Linq.Expressions;
namespace Core.Services
{
    public class TripFilterService
    {
        public Func<Trip, bool> BuildFilterPredicate(TripFilterParams filterParams)
        {
            return trip => (filterParams.TourID == null || trip.TourID == filterParams.TourID)
            && (string.IsNullOrWhiteSpace(filterParams.Keyword) || trip.Tour.Title.Contains(filterParams.Keyword, StringComparison.OrdinalIgnoreCase) || trip.Tour.Description.Contains(filterParams.Keyword, StringComparison.OrdinalIgnoreCase))
            && (filterParams.Starts == null || trip.StartTime.Date == filterParams.Starts.Value.Date)
            && (filterParams.Ends == null || trip.EndTime.Date == filterParams.Ends.Value.Date)
            && (filterParams.MinPrice == null || trip.GetSalePrice() >= filterParams.MinPrice)
            && (filterParams.MaxPrice == null || trip.GetSalePrice() <= filterParams.MaxPrice);
        }
    }
}
