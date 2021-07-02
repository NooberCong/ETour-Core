using Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Question : OwnedTrackedEntityWithKey<Customer, int, string>
    {
        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(1024, MinimumLength = 10)]
        public string Content { get; set; }
        public QuestionCategory? Category { get; set; }
        public QuestionPriority Priority { get; set; }
        public QuestionStatus Status { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

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
            Pending, // Mới hỏi nhân viên chưa trả lời
            Open, // Nv trả lời rồi nhưng chưa đủ kết thúc
            Closed // Đã giải đáp dc cho khách hàng
        }
    }
}
