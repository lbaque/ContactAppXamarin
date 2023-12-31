using ContactAppXamarin.Model.Interfaces;
using SQLite;
using System;

namespace ContactAppXamarin.Model.Base
{
    public abstract class BaseEntry<Tkey> : AuditEntry, IBaseEntry<Tkey>
    {
        [PrimaryKey]
        public virtual Tkey Id { get; set; } 
    }
}
