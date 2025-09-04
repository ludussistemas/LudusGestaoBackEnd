using FluentValidation;
using LudusGestao.Domain.DTOs.evento.Recebivel;

namespace LudusGestao.Domain.Validators.Recebivel
{
    public class CreateRecebivelValidator : AbstractValidator<CreateRecebivelDTO>
    {
        public CreateRecebivelValidator()
        {
            RuleFor(x => x.ClienteId).NotEmpty().WithMessage("O cliente é obrigatório.");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A descrição é obrigatória.");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
            RuleFor(x => x.DataVencimento).NotEmpty().WithMessage("A data de vencimento é obrigatória.");
            RuleFor(x => x.Situacao).NotEmpty().WithMessage("A situação é obrigatória.");
        }
    }
}
