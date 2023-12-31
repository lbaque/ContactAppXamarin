using Api.Contact.Domain.ContactoProfile;

namespace Api.Contact.Domain.UsuarioProfile
{
    public class UsuarioDTORequest
    {
        public string User { get; set; }
        public string Password { get; set; }
        public virtual List<ContactoDTORequest> Contacto { get; set; }
    }
}
