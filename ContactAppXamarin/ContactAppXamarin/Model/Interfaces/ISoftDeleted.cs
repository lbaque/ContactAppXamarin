using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Model.Interfaces
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
        Nullable<DateTime> DeletedAt { get; set; }
        Nullable<Guid> DeletedById { get; set; }
        bool Enabled { get; set; }
    }
}
