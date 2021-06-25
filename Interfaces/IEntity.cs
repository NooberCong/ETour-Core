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

    public interface IOwnedEntity<TOwner, TOwnerKey> where TOwner : IEntityWithKey<TOwnerKey>
    {
        public TOwner Owner { get; set; }
        public TOwnerKey OwnerID { get; set; }
    }

    public abstract class OwnedTrackedEntityWithKey<TOwner, TKey, TOwnerKey> : IOwnedEntity<TOwner, TOwnerKey>, IEntityWithKey<TKey>, ITrackedEntity where TOwner : IEntityWithKey<TOwnerKey>
    {
        public TOwner Owner { get; set; }
        public TKey ID { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual TOwnerKey OwnerID { get; set; }
    }

    public abstract class OwnedTrackedDeleteEntityWithKey<TOwner, TKey, TOwnerKey> : EntityWithKey<TKey>, IOwnedEntity<TOwner, TOwnerKey>, ITrackedEntity, ISoftDelete where TOwner : IEntityWithKey<TOwnerKey>
    {
        public virtual TOwner Owner { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual bool IsSoftDeleted { get; set; }
        public virtual TOwnerKey OwnerID { get; set; }
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
