using Infrastructure.Models.Base;

namespace Api.Contact.Models
{
    public class Contacto : BaseEntry<Guid>
    {
        public Contacto()
        {
            CreatedAt = DateTime.Now;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public byte[] Foto { get; set; }
        public Guid UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
