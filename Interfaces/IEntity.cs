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

    public interface IDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }

    public abstract class DeleteEntityWithKey<TKey> : EntityWithKey<TKey>, IDeleteEntity
    {
        public virtual bool IsDeleted { get; set; }
    }

    public interface IAuthoredEntity<TAuthor>
    {
        public TAuthor Author { get; set; }
    }

    public abstract class AuthoredTrackedDeleteEntityWithKey<TAuthor, TKey> : EntityWithKey<TKey>, IAuthoredEntity<TAuthor>, ITrackedEntity, IDeleteEntity
    {
        public virtual TAuthor Author { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual bool IsDeleted { get; set; }
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
