using FluentValidation;
using LudusGestao.Domain.DTOs.Usuario;

namespace LudusGestao.Domain.Validators.geral.Usuario
{
    public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioDTO>
    {
        public UpdateUsuarioValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome do usuário é obrigatório.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail inválido.");
            RuleFor(x => x.EmpresaId).NotEmpty().WithMessage("A empresa é obrigatória.");
        }
    }
}
