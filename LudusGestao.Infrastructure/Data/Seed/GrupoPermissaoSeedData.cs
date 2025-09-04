using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class GrupoPermissaoSeedData
    {
        public static List<GrupoPermissao> GetGrupos(int tenantId)
        {
            return new List<GrupoPermissao>
            {
                new GrupoPermissao
                {
                    Id = Guid.NewGuid(),
                    Nome = "Administrador",
                    Descricao = "Acesso total ao sistema - pode gerenciar tudo",
                    Situacao = SituacaoBase.Ativo,
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                },
                new GrupoPermissao
                {
                    Id = Guid.NewGuid(),
                    Nome = "Gerente",
                    Descricao = "Gerencia reservas, clientes e locais - sem acesso a configurações",
                    Situacao = SituacaoBase.Ativo,
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                },
                new GrupoPermissao
                {
                    Id = Guid.NewGuid(),
                    Nome = "Operador",
                    Descricao = "Operações básicas de reservas e atendimento",
                    Situacao = SituacaoBase.Ativo,
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                },
                new GrupoPermissao
                {
                    Id = Guid.NewGuid(),
                    Nome = "Atendente",
                    Descricao = "Atendimento básico - visualização e criação simples",
                    Situacao = SituacaoBase.Ativo,
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                }
            };
        }
    }
}
