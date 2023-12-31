using Api.Contact.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Contact.Context.Configuration
{
    public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.HasKey(e => new { e.Id });
            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasData(
                new Contacto() { Id = new Guid("f3c1d434-ba7e-4fc9-be72-dbdd3901bd09"), Nombre = "Alaska", Apellido = "Young", Celular = "+593963689656", Telefono = "02609639", UsuarioId = new Guid("43aeeeda-9961-4047-9a6c-fb40aed780cc") },
                new Contacto() { Id = new Guid("1489cbe9-d763-434e-ba9e-1149555e35db"), Nombre = "Danna", Apellido = "Tigua", Celular = "+593977689656", Telefono = "02607739", UsuarioId = new Guid("1128630a-da70-4242-8a56-ff0b7d38dc1d") },
                new Contacto() { Id = new Guid("52a69d0a-3017-42c2-ad61-0a89237ce452"), Nombre = "Milena", Apellido = "Castro", Celular = "+593964489656", Telefono = "02688639", UsuarioId = new Guid("5ef58fa0-e108-4d8c-aa1e-8640813deb15") }
                );
        }
    }
}
