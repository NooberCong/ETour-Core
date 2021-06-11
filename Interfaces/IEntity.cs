using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Interfaces
{
    public interface IEntityWithKey<TKey>
    {
        public TKey ID { get; set; }
    }

    public abstract class EntityWithKey<TKey> : IEntityWithKey<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey ID { get; set; }
    }

    public interface ISoftDelete
    {
        public bool IsSoftDeleted { get; set; }
    }

    public abstract class SoftDeleteEntityWithKey<TKey> : EntityWithKey<TKey>, ISoftDelete
    {
        public virtual bool IsSoftDeleted { get; set; }
    }

    public interface IAuthoredEntity<TAuthor, TAuthorKey> where TAuthor : IEntityWithKey<TAuthorKey>
    {
        public TAuthor Author { get; set; }
        public TAuthorKey AuthorID { get; set; }
    }

    public abstract class AuthoredTrackedEntityWithKey<TAuthor, TKey, TAuthorKey> : IAuthoredEntity<TAuthor, TAuthorKey>, IEntityWithKey<TKey>, ITrackedEntity where TAuthor : IEntityWithKey<TAuthorKey>
    {
        public TAuthor Author { get; set; }
        public TKey ID { get; set; }
        public DateTime LastUpdated { get; set; }
        [Required]
        public virtual TAuthorKey AuthorID { get; set; }
    }

    public abstract class AuthoredTrackedDeleteEntityWithKey<TAuthor, TKey, TAuthorKey> : EntityWithKey<TKey>, IAuthoredEntity<TAuthor, TAuthorKey>, ITrackedEntity, ISoftDelete where TAuthor : IEntityWithKey<TAuthorKey>
    {
        public virtual TAuthor Author { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual bool IsSoftDeleted { get; set; }
        [Required]
        public virtual TAuthorKey AuthorID { get; set; }
    }

    public interface ITrackedEntity
    {
        public DateTime LastUpdated { get; set; }
    }

    public abstract class TrackedEntityWithKey<TKey> : EntityWithKey<TKey>, ITrackedEntity
    {
        public virtual DateTime LastUpdated { get; set; }
    }
}
