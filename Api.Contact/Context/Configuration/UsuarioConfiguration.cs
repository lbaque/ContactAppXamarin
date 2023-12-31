using Api.Contact.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Contact.Context.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => new { e.Id });
            builder.HasQueryFilter(x => !x.Deleted);

            builder
                .HasMany(x => x.Contacto)
                .WithOne(x => x.Usuario)
                .HasForeignKey(i => i.UsuarioId)
                .IsRequired(true);

                builder.HasData(
                 new Usuario()
                 {
                     Id = new Guid("43aeeeda-9961-4047-9a6c-fb40aed780cc"),
                     User = "ayoung",
                     Password = "0598BF5B847F0297328C2F6F77CEC86C41DF1AD704FCE9B429CAC28019FEA76B"

                 },
                 new Usuario()
                 {
                     Id = new Guid("1128630a-da70-4242-8a56-ff0b7d38dc1d"),
                     User = "dtigua",
                     Password = "1EC565DA06E86121A4FA61DFA4627AF44CB2B6C949EB208859AA42D2DD76A343",
                 },
                 new Usuario()
                 {
                     Id = new Guid("5ef58fa0-e108-4d8c-aa1e-8640813deb15"),
                     User = "mcastro",
                     Password = "2D0BB132CDF675AB1EC10806D332A85E9AEF84C4F73A2214D5318BC6C5905210"
                 }
            );
        }
    }
}
