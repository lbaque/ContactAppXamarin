using Api.Contact.Domain.UsuarioProfile;
using Api.Contact.Models;
using AutoMapper;

namespace Api.Contact.Domain.ContactoProfile
{
    public class ContactoProfileConfig : Profile
    {
        public ContactoProfileConfig()
        {
            CreateMap<Contacto, ContactoDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            CreateMap<Contacto, ContactoDTORequest>().ReverseMap();
            CreateMap<Usuario, UsuarioDTORequest>().ReverseMap();

            CreateMap<Contacto, ContactoDTOUpdate>().ReverseMap();
            CreateMap<Usuario, UsuarioDTOUpdate>().ReverseMap();

        }
    }
}
