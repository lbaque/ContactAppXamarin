using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Model.Interfaces
{
    public interface IAuditEntry
    {
        DateTime CreatedAt { get; set; }
        Guid CreatedById { get; set; }
        Nullable<DateTime> UpdatedAt { get; set; }
        Nullable<Guid> UpdatedById { get; set; }
    }
}
