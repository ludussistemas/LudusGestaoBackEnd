using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class UsuarioSeedData
    {
        public static Usuario GetUsuarioAdmin(Guid empresaId, Guid? grupoPermissaoId, int tenantId)
        {
            return new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Admin Ludus",
                Email = "admin@ludus.com.br",
                Telefone = "(11) 77777-7777",
                Cargo = "Administrador",
                EmpresaId = empresaId,
                GrupoPermissaoId = grupoPermissaoId,
                Situacao = SituacaoBase.Ativo,
                UltimoAcesso = DateTime.UtcNow,
                Senha = BCrypt.Net.BCrypt.HashPassword("Ludus@2024"),
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
