using Infrastructure.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models.Base
{
    public abstract class BaseEntry<Tkey> : AuditEntry, IBaseEntry<Tkey>, IEntity
    {
        [Key]
        [Column(Order = 1)]
        public virtual Tkey Id { get; set; }
    }
}
