using FluentValidation;
using LudusGestao.Domain.DTOs.Empresa;

namespace LudusGestao.Domain.Validators.geral.Empresa
{
    public class CreateEmpresaValidator : AbstractValidator<CreateEmpresaDTO>
    {
        public CreateEmpresaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome da empresa é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome deve ter no máximo 100 caracteres")
                .MinimumLength(2)
                .WithMessage("Nome deve ter no mínimo 2 caracteres");

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage("CNPJ é obrigatório")
                .Length(18)
                .WithMessage("CNPJ deve ter 18 caracteres (formato: XX.XXX.XXX/XXXX-XX)")
                .Must(BeValidCnpj)
                .WithMessage("CNPJ inválido");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .EmailAddress()
                .WithMessage("Email deve ter um formato válido")
                .MaximumLength(100)
                .WithMessage("Email deve ter no máximo 100 caracteres");

            RuleFor(x => x.Rua)
                .NotEmpty()
                .WithMessage("Rua é obrigatória")
                .MaximumLength(100)
                .WithMessage("Rua deve ter no máximo 100 caracteres");

            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("Número é obrigatório")
                .MaximumLength(10)
                .WithMessage("Número deve ter no máximo 10 caracteres");

            RuleFor(x => x.Bairro)
                .NotEmpty()
                .WithMessage("Bairro é obrigatório")
                .MaximumLength(50)
                .WithMessage("Bairro deve ter no máximo 50 caracteres");

            RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage("Cidade é obrigatória")
                .MaximumLength(50)
                .WithMessage("Cidade deve ter no máximo 50 caracteres");

            RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage("Estado é obrigatório")
                .Length(2)
                .WithMessage("Estado deve ter 2 caracteres (sigla)");

            RuleFor(x => x.CEP)
                .NotEmpty()
                .WithMessage("CEP é obrigatório")
                .Length(9)
                .WithMessage("CEP deve ter 9 caracteres (formato: XXXXX-XXX)")
                .Must(BeValidCep)
                .WithMessage("CEP inválido");
        }

        private bool BeValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres especiais
            var cnpjLimpo = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            if (cnpjLimpo.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpjLimpo.All(c => c == cnpjLimpo[0]))
                return false;

            // Validação dos dígitos verificadores
            var soma = 0;
            var peso = 2;

            for (int i = 11; i >= 0; i--)
            {
                soma += int.Parse(cnpjLimpo[i].ToString()) * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cnpjLimpo[12].ToString()) != digito1)
                return false;

            soma = 0;
            peso = 2;

            for (int i = 12; i >= 0; i--)
            {
                soma += int.Parse(cnpjLimpo[i].ToString()) * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cnpjLimpo[13].ToString()) == digito2;
        }

        private bool BeValidCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            // Remove caracteres especiais
            var cepLimpo = cep.Replace("-", "");

            if (cepLimpo.Length != 8)
                return false;

            // Verifica se contém apenas números
            return cepLimpo.All(char.IsDigit);
        }
    }
}
