using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models.Interfaces
{
    public interface IAuditEntry
    {
        DateTime CreatedAt { get; set; }
        Guid CreatedById { get; set; }
        Nullable<DateTime> UpdatedAt { get; set; }
        Nullable<Guid> UpdatedById { get; set; }
    }
}
