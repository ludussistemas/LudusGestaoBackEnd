using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class PermissaoSeedData
    {
        public static List<Modulo> GetModulos()
        {
            return new List<Modulo>
            {
                new Modulo { Id = Guid.NewGuid(), Nome = "Eventos" },
                new Modulo { Id = Guid.NewGuid(), Nome = "Configuracoes" }
            };
        }

        public static List<Submodulo> GetSubmodulos(Guid moduloEventosId, Guid moduloConfiguracoesId)
        {
            return new List<Submodulo>
            {
                // Eventos - Locais
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloEventosId, Nome = "Locais" },
                // Eventos - Clientes
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloEventosId, Nome = "Clientes" },
                // Eventos - Recebiveis
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloEventosId, Nome = "Recebiveis" },
                // Eventos - Reservas
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloEventosId, Nome = "Reservas" },

                // Configuracoes - Usuarios
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloConfiguracoesId, Nome = "Usuarios" },
                // Configuracoes - Filiais
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloConfiguracoesId, Nome = "Filiais" },
                // Configuracoes - Empresa
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloConfiguracoesId, Nome = "Empresa" },
                // Configuracoes - Grupo de Permissões
                new Submodulo { Id = Guid.NewGuid(), ModuloId = moduloConfiguracoesId, Nome = "Grupo de Permissões" }
            };
        }

        public static List<Acao> GetAcoes()
        {
            return new List<Acao>
            {
                new Acao { Id = Guid.NewGuid(), Nome = "Criar", Descricao = "Permite criar novos registros" },
                new Acao { Id = Guid.NewGuid(), Nome = "Editar", Descricao = "Permite editar registros existentes" },
                new Acao { Id = Guid.NewGuid(), Nome = "Excluir", Descricao = "Permite excluir registros" },
                new Acao { Id = Guid.NewGuid(), Nome = "Visualizar", Descricao = "Permite visualizar registros" },
                new Acao { Id = Guid.NewGuid(), Nome = "Acesso", Descricao = "Permite acesso ao módulo" }
            };
        }
    }
}
