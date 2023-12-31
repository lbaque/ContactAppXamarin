using ContactAppXamarin.Model.Interfaces;
using System;

namespace ContactAppXamarin.Model.Base
{
    public class SoftDeleted : ISoftDeleted
    {
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedById { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
