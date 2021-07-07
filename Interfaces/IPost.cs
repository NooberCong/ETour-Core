using Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public interface IPost<TAuthor> : IEntityWithKey<int>, ISoftDelete, ITrackedEntity, IOwnedEntity<TAuthor, string> where TAuthor : IEmployee
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverImgUrl { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> Tags { get; set; }
        public PostCategory Category { get; set; }

        [ForeignKey("PostID")]
        public ICollection<Comment> Comments { get; set; }

        public enum PostCategory
        {
            Destinations,
            Travel_Planning,
            Family_Travel,
            Travel_Gear,
            Money_Management,
            Travel_Inspiration
        }
    }
}
