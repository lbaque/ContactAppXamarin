using Infrastructure.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Base
{
    public class AuditEntry : SoftDeleted, IAuditEntry, IEntity
    {
        [Required(ErrorMessage = "Debe llenar la fecha de ingreso")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Debe llenar el usuario de ingreso")]
        public Guid CreatedById { get; set; }

        public Nullable<DateTime> UpdatedAt { get; set; }

        public Nullable<Guid> UpdatedById { get; set; }
    }
}
