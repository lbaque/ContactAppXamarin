using ContactAppXamarin.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Model
{
    public class UsuarioModel : BaseEntry<Guid>
    {
        public UsuarioModel()
        {
            CreatedAt = DateTime.Now;
        }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
