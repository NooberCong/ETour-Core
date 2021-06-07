using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Question : AuthoredTrackedEntityWithKey<Customer, int, string>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public QuestionCategory Category { get; set; }
        public QuestionPriority Priority { get; set; }
        public QuestionStatus Status { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public enum QuestionCategory
        {
            Account,
            Travel,
            Order,
            Other
        }

        public enum QuestionPriority
        {
            High,
            Medium,
            Low
        }

        public enum QuestionStatus
        {
            Pending,
            Open,
            Closed
        }
    }
}
