using Core.Entities;
using System;
using System.Linq;

namespace Core.Services
{
    public class AnalyticsService
    {
        public CrossYearRevenueReport CrossYearMonthlyRevenue(IQueryable<Booking> bookings, DateTime currentDate)
        {
            int endMonth = currentDate.Month;
            int endYear = currentDate.Year;

            (int startMonth, int startYear) = GetStartMonthYear(endMonth, endYear);

            CrossYearRevenueReport report = new CrossYearRevenueReport
            {
                StartMonth = startMonth,
                StartYear = startYear,
                EndMonth = endMonth,
                EndYear = endYear,
            };

            MonthlyStats(startMonth, startYear, endMonth, endYear, bookings, m => report.SetThisYearMonth(m.Key, m.Sum(bk => bk.Total)));
            MonthlyStats(startMonth, startYear - 1, endMonth, endYear - 1, bookings, m => report.SetLastYearMonth(m.Key, m.Sum(bk => bk.Total)));

            return report;
        }

        public CrossYearTicketSalesReport CrossYearMonthlyTicketSales(IQueryable<Booking> bookings, DateTime currentDate)
        {
            int endMonth = currentDate.Month;
            int endYear = currentDate.Year;

            (int startMonth, int startYear) = GetStartMonthYear(endMonth, endYear);

            CrossYearTicketSalesReport report = new CrossYearTicketSalesReport
            {
                StartMonth = startMonth,
                StartYear = startYear,
                EndMonth = endMonth,
                EndYear = endYear,
            };

            MonthlyStats(startMonth, startYear, endMonth, endYear, bookings, m => report.SetThisYearMonth(m.Key, m.Sum(bk => bk.TicketCount)));
            MonthlyStats(startMonth, startYear - 1, endMonth, endYear - 1, bookings, m => report.SetLastYearMonth(m.Key, m.Sum(bk => bk.TicketCount)));

            return report;
        }

        public Tour[] TopBookedTour(int top, IQueryable<Booking> bookings, DateTime start, DateTime end)
        {
            return bookings.Where(bk => bk.LastUpdated >= start && bk.LastUpdated <= end)
                .AsEnumerable()
                .GroupBy(bk => bk.Trip.TourID)
                .OrderByDescending(gr => gr.Sum(bk => bk.TicketCount))
                .Take(top).Select(gr => gr.First().Trip.Tour).ToArray();
        }

        public BookingStatusSegmentation BookingStatusSegmentation(IQueryable<Booking> bookings)
        {
            BookingStatusSegmentation segmentation = new();

            foreach (var gr in bookings.AsEnumerable().GroupBy(bk => bk.Status))
            {
                segmentation.SetSegment(gr.Key, gr.Count());
            }

            return segmentation;
        }

        private (int startMonth, int startYear) GetStartMonthYear(int endMonth, int endYear)
        {
            return ((endMonth + 1) % 12, endYear - (endMonth < 12 ? 1 : 0));
        }

        private void MonthlyStats(int startMonth, int startYear, int endMonth, int endYear, IQueryable<Booking> bookings, Action<IGrouping<int, Booking>> aggregator)
        {

            DateTime start = new DateTime(startYear, startMonth, 1);
            DateTime end = new DateTime(endYear, endMonth, DateTime.DaysInMonth(endYear, endMonth));

            var months = bookings.Where(bk => bk.LastUpdated >= start && bk.LastUpdated <= end)
                .AsEnumerable()
                .GroupBy(bk => bk.LastUpdated.Month);

            foreach (var month in months)
            {
                aggregator.Invoke(month);
            }
        }
    }
}
