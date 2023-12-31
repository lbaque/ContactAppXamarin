using Api.Contact.Domain.ContactoProfile;
using Api.Contact.Models;

namespace Api.Contact.Domain.UsuarioProfile
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        
    }
}
