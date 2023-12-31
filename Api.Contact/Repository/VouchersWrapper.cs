using Api.Contact.Context;
using Api.Contact.Models;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Interfaces;

namespace Api.Contact.Repository
{
    public class VouchersWrapper : IVouchersWrapper
    {
        private ApiContactContext _context;

        public VouchersWrapper(ApiContactContext context)
        {
            _context = context;
        }

        private IBaseRepository<Contacto> _contacto;
        private IBaseRepository<Usuario> _usuario;

        public IBaseRepository<Contacto> Contacto { get { if (_contacto == null) _contacto = new BaseRepository<Contacto>(_context); return _contacto; } }
        public IBaseRepository<Usuario> Usuario { get { if (_usuario == null) _usuario = new BaseRepository<Usuario>(_context); return _usuario; } }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
