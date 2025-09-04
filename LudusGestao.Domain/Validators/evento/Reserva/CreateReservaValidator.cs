using FluentValidation;
using LudusGestao.Domain.DTOs.reserva.Reservas;

namespace LudusGestao.Domain.Validators.evento.Reserva
{
    public class CreateReservaValidator : AbstractValidator<CreateReservaDTO>
    {
        public CreateReservaValidator()
        {
            RuleFor(x => x.ClienteId).NotEmpty().WithMessage("O cliente é obrigatório.");
            RuleFor(x => x.LocalId).NotEmpty().WithMessage("O local é obrigatório.");
            RuleFor(x => x.Data).NotEmpty().WithMessage("A data é obrigatória.");
            RuleFor(x => x.HoraInicio).NotEmpty().WithMessage("A hora de início é obrigatória.");
            RuleFor(x => x.HoraFim).NotEmpty().WithMessage("A hora de fim é obrigatória.");
            RuleFor(x => x.Situacao).NotEmpty().WithMessage("A situação é obrigatória.");
        }
    }
}
