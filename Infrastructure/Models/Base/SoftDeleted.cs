using Infrastructure.Models.Interfaces;
using System;
using System.ComponentModel;

namespace Infrastructure.Models.Base
{
    public class SoftDeleted : ISoftDeleted
    {
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid? DeletedById { get; set; }

        public bool Enabled { get; set; } = true;

    }
}
