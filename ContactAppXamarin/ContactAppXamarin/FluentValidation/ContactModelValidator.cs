using ContactAppXamarin.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.FluentValidation
{
    public class ContactModelValidator : AbstractValidator<ContactoModel>
    {
        public ContactModelValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("El teléfono es obligatorio");
            RuleFor(x => x.Celular).NotEmpty().WithMessage("El número celular es obligatorio");
        }
    }
}
