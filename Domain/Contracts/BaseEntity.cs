using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class BaseEntity<TId> : IEntity<TId>
        where TId : notnull
    {
        public TId Id { get; set; }
        public BaseEntity()
        {
        }
        public BaseEntity(TId id)
        {
            Id = id;
        }
    }
}
