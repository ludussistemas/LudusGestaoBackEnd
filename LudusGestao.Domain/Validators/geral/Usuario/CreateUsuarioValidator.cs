using FluentValidation;
using LudusGestao.Domain.DTOs.Usuario;

namespace LudusGestao.Domain.Validators.geral.Usuario
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioDTO>
    {
        public CreateUsuarioValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do usuário é obrigatório.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail inválido.");
            RuleFor(x => x.Senha).NotEmpty().WithMessage("A senha é obrigatória.");
            RuleFor(x => x.EmpresaId).NotEmpty().WithMessage("A empresa é obrigatória.");
        }
    }
}
