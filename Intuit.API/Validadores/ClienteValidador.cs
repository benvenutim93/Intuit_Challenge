using FluentValidation;
using Intuit.Models;

namespace Intuit.API.Validadores
{
    public class ClienteValidador : AbstractValidator<Cliente>
    {
        public ClienteValidador()
        {
            RuleFor(a => a.Nombres).NotEmpty().WithMessage("Nombre no puede estar vacio");
            RuleFor(a => a.Apellidos).NotEmpty().WithMessage("Apellido no puede estar vacio");
            RuleFor(a => a.FechaNacimiento).NotEmpty().WithMessage("Fecha de Nacimiento no puede estar vacio");
            RuleFor(a => a.Email).EmailAddress().WithMessage("Direccion de mail inválida");
            RuleFor(a => a.Cuit).MinimumLength(11).MaximumLength(11).WithMessage("El Cuit debe tener 11 caracteres");
        }
    }
}
