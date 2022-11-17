using System;
using System.ComponentModel.DataAnnotations;

namespace Review.Domain.Common
{
    public interface IBaseEntity
    { }

    public abstract class BaseEntity<T> : IBaseEntity
    {
        public BaseEntity()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        public T Id { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    { }
}
