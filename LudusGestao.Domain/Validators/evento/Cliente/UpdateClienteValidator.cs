using FluentValidation;
using LudusGestao.Domain.DTOs.reserva.Cliente;

namespace LudusGestao.Domain.Validators.Cliente
{
    public class UpdateClienteValidator : AbstractValidator<UpdateClienteDTO>
    {
        public UpdateClienteValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do cliente é obrigatório.");
            RuleFor(x => x.Documento).NotEmpty().WithMessage("O documento é obrigatório.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail inválido.");
            RuleFor(x => x.Situacao).NotEmpty().WithMessage("A situação é obrigatória.");
        }
    }
}
