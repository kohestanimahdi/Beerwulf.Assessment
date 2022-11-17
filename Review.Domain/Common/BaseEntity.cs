using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
