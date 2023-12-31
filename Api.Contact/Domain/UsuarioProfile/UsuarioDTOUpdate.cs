using Api.Contact.Domain.ContactoProfile;

namespace Api.Contact.Domain.UsuarioProfile
{
    public class UsuarioDTOUpdate
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public virtual ContactoDTOUpdate Contacto { get; set; }
    }
}
