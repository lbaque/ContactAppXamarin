using ContactAppXamarin.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Model.Base
{
    public class AuditEntry: SoftDeleted, IAuditEntry
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<Guid> UpdatedById { get; set; }
        public bool synchronized { get; set; } = false;
    }
}
