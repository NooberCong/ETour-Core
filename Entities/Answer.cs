using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Answer : TrackedEntityWithKey<int>
    {
        public string Author { get; set; }
        public bool AuthoredByCustomer { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 3)]
        public string Content { get; set; }
        public int QuestionID { get; set; }
    }
}
