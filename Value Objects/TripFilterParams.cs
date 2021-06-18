using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Value_Objects
{
    public record TripFilterParams
    {
        public string Keyword { get; set; }
        public int? TourID { get; set; }
        public Tour.TourType? TourType { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Starts { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Ends { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinRating { get; set; }
    }
}
