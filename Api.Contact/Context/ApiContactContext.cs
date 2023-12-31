using Api.Contact.Context.Configuration;
using Api.Contact.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Contact.Context
{
    public class ApiContactContext : DbContext
    {
        public virtual DbSet<Usuario> Contacto { get; set; }
        public virtual DbSet<Usuario> Agenda { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public ApiContactContext(DbContextOptions options)
         : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //-------------------------------Configuration----------------------------------------------//
            modelBuilder.ApplyConfiguration(new ContactoConfiguration());            
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
           
            

            // Global turn off delete behaviour on foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

        }
    }
}
