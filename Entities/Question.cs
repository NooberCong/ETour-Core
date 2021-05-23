using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Question: AuthoredTrackedDeleteEntityWithKey<Customer, int>
    {
        public string Content { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
