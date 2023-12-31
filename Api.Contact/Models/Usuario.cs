using Infrastructure.Models.Base;

namespace Api.Contact.Models
{
    public class Usuario : BaseEntry<Guid>
    {
        public Usuario()
        {
            CreatedAt = DateTime.Now;
        }
        public string User { get; set; }
        public string Password { get; set; }
        public virtual List<Contacto> Contacto { get; set; }
    }
}
