using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models.Interfaces
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
        Nullable<DateTime> DeletedAt { get; set; }
        Nullable<Guid> DeletedById { get; set; }
        bool Enabled { get; set; }
    }
}
