using Api.Contact.Models;
using Infrastructure.Repository.Interfaces;
using System.Diagnostics.Contracts;

namespace Api.Contact.Repository
{
    public interface IVouchersWrapper
    {
        public IBaseRepository<Contacto> Contacto { get; }
        public IBaseRepository<Usuario> Usuario { get; }

        void Save();
    }
}
