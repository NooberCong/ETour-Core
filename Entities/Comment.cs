using Core.Entities;
using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Comment: OwnedTrackedEntityWithKey<Customer, int, string>
    {
        public int PostID { get; set; }
        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Content { get; set; }
    }
}
