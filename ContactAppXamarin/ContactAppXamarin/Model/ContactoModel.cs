using ContactAppXamarin.Model.Base;
using System;

namespace ContactAppXamarin.Model
{
    public class ContactoModel:BaseEntry<Guid>
    {
        public ContactoModel()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public byte[] Foto { get; set; }
        public Guid UsuarioId { get; set; }

    }
}
