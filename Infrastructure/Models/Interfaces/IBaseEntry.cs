using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models.Interfaces
{
    public interface IBaseEntry<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}
