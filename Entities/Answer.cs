using Core.Interfaces;

namespace Core.Entities
{
    public class Answer : TrackedEntityWithKey<int>
    {
        public string Author { get; set; }
        public bool AuthoredByCustomer { get; set; }
        public string Content { get; set; }
        public int QuestionID { get; set; }
    }
}
