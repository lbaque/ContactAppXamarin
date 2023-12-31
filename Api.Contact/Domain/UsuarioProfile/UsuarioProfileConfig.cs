using Api.Contact.Domain.ContactoProfile;
using Api.Contact.Models;
using AutoMapper;

namespace Api.Contact.Domain.UsuarioProfile
{
    public class UsuarioProfileConfig : Profile
    {
        public UsuarioProfileConfig()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Contacto, ContactoDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioDTORequest>().ReverseMap();
            CreateMap<Contacto, ContactoDTORequest>().ReverseMap();
            
            CreateMap<Usuario, UsuarioDTOUpdate>().ReverseMap();
            CreateMap<Contacto, ContactoDTOUpdate>().ReverseMap();
        }
    }
}
