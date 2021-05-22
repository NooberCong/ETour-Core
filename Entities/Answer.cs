using Core.Interfaces;

namespace Core.Entities
{
    public class Answer: AuthoredTrackedDeleteEntityWithKey<string, int>
    {
        public bool AuthoredByCustomer { get; set; }
        public string Content { get; set; }
        public int QuestionID { get; set; }
    }
}
