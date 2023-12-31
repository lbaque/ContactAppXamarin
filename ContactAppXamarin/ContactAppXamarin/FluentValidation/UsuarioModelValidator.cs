using ContactAppXamarin.Model;
using FluentValidation;

namespace ContactAppXamarin.FluentValidation
{
    public class UsuarioModelValidator : AbstractValidator<UsuarioModel>
    {
        public UsuarioModelValidator()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage("El user es obligatorio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("El password es obligatorio");
            
        }
    }
}
