using Core.Interfaces;

namespace Core.Entities
{
    public interface IPost<TAuthor> : IEntityWithKey<int>, IDeleteEntity, ITrackedEntity, IAuthoredEntity<TAuthor> where TAuthor : IEmployee
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverImgUrl { get; set; }
    }
}
