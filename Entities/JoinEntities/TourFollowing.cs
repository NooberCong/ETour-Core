namespace Core.Entities
{
    public class TourFollowing
    {
        public Tour Tour { get; set; }
        public int TourID { get; set; }
        public Customer Customer { get; set; }
        public string CustomerID { get; set; }
    }
}
