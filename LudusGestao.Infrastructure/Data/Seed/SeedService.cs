using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.eventos;
using LudusGestao.Domain.Enums.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public class SeedService : ISeedService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public SeedService(ApplicationDbContext context, ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<bool> SeedDadosBaseAsync()
        {
            try
            {
                // Garantir que o filtro global de tenant tenha um TenantId válido durante o seed
                _tenantService.SetTenant("1");

                // Verificar se já existem dados
                if (await _context.Empresas.AnyAsync())
                {
                    return false; // Dados já existem
                }

                // 1. Seed de permissões
                await SeedPermissoesAsync();

                // 2. Seed de grupos de permissões
                await SeedGruposAsync();

                // 3. Seed de dados básicos (empresa, filial, usuário admin)
                await SeedDadosBasicosAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro no seed: {ex.Message}");
                return false;
            }
        }

        private async Task SeedPermissoesAsync()
        {
            if (await _context.Permissoes.AnyAsync())
                return;

            var permissoes = new List<Permissao>();

            // MÓDULO: CONFIGURAÇÕES
            permissoes.AddRange(CriarPermissoesModulo("configuracoes", new[]
            {
                ("acesso", "Acesso ao módulo de configurações"),
                ("empresa.visualizar", "Visualizar dados da empresa"),
                ("empresa.editar", "Editar dados da empresa"),
                ("empresa.excluir", "Excluir empresa"),
                ("filial.visualizar", "Visualizar filiais"),
                ("filial.criar", "Criar nova filial"),
                ("filial.editar", "Editar filial"),
                ("filial.excluir", "Excluir filial"),
                ("usuario.visualizar", "Visualizar usuários"),
                ("usuario.criar", "Criar usuário"),
                ("usuario.editar", "Editar usuário"),
                ("usuario.excluir", "Excluir usuário"),
                ("usuario.permissoes", "Gerenciar permissões de usuário"),
                ("grupo.visualizar", "Visualizar grupos de permissões"),
                ("grupo.criar", "Criar grupo de permissões"),
                ("grupo.editar", "Editar grupo de permissões"),
                ("grupo.excluir", "Excluir grupo de permissões"),
                ("grupo.permissoes", "Gerenciar permissões do grupo"),
                ("grupo.usuarios", "Gerenciar usuários do grupo")
            }));

            // MÓDULO: RESERVAS
            permissoes.AddRange(CriarPermissoesModulo("reservas", new[]
            {
                ("acesso", "Acesso ao módulo de reservas"),
                ("cliente.visualizar", "Visualizar clientes"),
                ("cliente.criar", "Criar cliente"),
                ("cliente.editar", "Editar cliente"),
                ("cliente.excluir", "Excluir cliente"),
                ("local.visualizar", "Visualizar locais"),
                ("local.criar", "Criar local"),
                ("local.editar", "Editar local"),
                ("local.excluir", "Excluir local"),
                ("reserva.visualizar", "Visualizar reservas"),
                ("reserva.criar", "Criar reserva"),
                ("reserva.editar", "Editar reserva"),
                ("reserva.cancelar", "Cancelar reserva"),
                ("reserva.confirmar", "Confirmar reserva"),
                ("recebivel.visualizar", "Visualizar recebíveis"),
                ("recebivel.criar", "Criar recebível"),
                ("recebivel.editar", "Editar recebível"),
                ("recebivel.excluir", "Excluir recebível"),
                ("recebivel.marcar_pago", "Marcar como pago"),
                ("relatorio.visualizar", "Visualizar relatórios"),
                ("relatorio.exportar", "Exportar relatórios")
            }));

            await _context.Permissoes.AddRangeAsync(permissoes);
            await _context.SaveChangesAsync();
        }

        private async Task SeedGruposAsync()
        {
            if (await _context.GruposPermissoes.AnyAsync())
                return;

            var tenantId = 1;
            var grupos = new List<GrupoPermissao>();

            // 1. ADMINISTRADOR
            var admin = new GrupoPermissao
            {
                Id = Guid.NewGuid(),
                Nome = "Administrador",
                Descricao = "Acesso total ao sistema - pode gerenciar tudo",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
            grupos.Add(admin);

            // 2. GERENTE
            var gerente = new GrupoPermissao
            {
                Id = Guid.NewGuid(),
                Nome = "Gerente",
                Descricao = "Gerencia reservas, clientes e locais - sem acesso a configurações",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
            grupos.Add(gerente);

            // 3. OPERADOR
            var operador = new GrupoPermissao
            {
                Id = Guid.NewGuid(),
                Nome = "Operador",
                Descricao = "Operações básicas de reservas e atendimento",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
            grupos.Add(operador);

            // 4. ATENDENTE
            var atendente = new GrupoPermissao
            {
                Id = Guid.NewGuid(),
                Nome = "Atendente",
                Descricao = "Atendimento básico - visualização e criação simples",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
            grupos.Add(atendente);

            await _context.GruposPermissoes.AddRangeAsync(grupos);
            await _context.SaveChangesAsync();
        }

        private async Task SeedDadosBasicosAsync()
        {
            var tenantId = 1;

            // 1. Criar Empresa
            var empresa = new Empresa
            {
                Id = Guid.NewGuid(),
                Nome = "Ludus Sistemas",
                Cnpj = "12345678000199",
                Email = "contato@ludus.com.br",
                Endereco = "Rua Exemplo, 100",
                Cidade = "CidadeX",
                Estado = "SP",
                Cep = "00000-000",
                Telefone = "(11) 99999-9999",
                Situacao = SituacaoBase.Ativo,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Empresas.AddAsync(empresa);

            // 2. Criar Filial
            var filial = new Filial
            {
                Id = Guid.NewGuid(),
                Nome = "Filial Central",
                Codigo = "F001",
                Endereco = "Rua Central, 200",
                Cidade = "CidadeX",
                Estado = "SP",
                Cep = "00000-001",
                Telefone = "(11) 88888-8888",
                Email = "filial@ludus.com.br",
                Cnpj = "12345678000199",
                Responsavel = "João Gerente",
                DataAbertura = DateTime.UtcNow,
                Situacao = SituacaoBase.Ativo,
                EmpresaId = empresa.Id,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Filiais.AddAsync(filial);

            // 3. Obter grupo administrador
            var grupoAdmin = await _context.GruposPermissoes
                .FirstOrDefaultAsync(g => g.Nome == "Administrador");

            // 4. Vincular grupo Administrador à filial com TODAS as permissões
            if (grupoAdmin != null)
            {
                var todasPermissoes = await _context.Permissoes.ToListAsync();

                var grupoPermissaoFilial = new GrupoPermissaoFilial
                {
                    Id = Guid.NewGuid(),
                    GrupoPermissaoId = grupoAdmin.Id,
                    FilialId = filial.Id,
                    Permissoes = todasPermissoes.Select(p => p.Id).ToList(),
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                };

                await _context.GruposPermissoesFiliais.AddAsync(grupoPermissaoFilial);
            }

            // 5. Criar Usuário Administrador
            var usuarioAdmin = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Admin Ludus",
                Email = "admin@ludus.com.br",
                Telefone = "(11) 77777-7777",
                Cargo = "Administrador",
                EmpresaId = empresa.Id,
                GrupoPermissaoId = grupoAdmin?.Id,
                Situacao = SituacaoBase.Ativo,
                UltimoAcesso = DateTime.UtcNow,
                Senha = BCrypt.Net.BCrypt.HashPassword("Ludus@2024"),
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Usuarios.AddAsync(usuarioAdmin);

            // 5. Criar Cliente Exemplo
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = "Cliente Exemplo",
                Documento = "12345678901",
                Email = "cliente@exemplo.com",
                Telefone = "(11) 66666-6666",
                Endereco = "Rua Cliente, 300",
                Observacoes = "Cliente VIP",
                Situacao = SituacaoCliente.Ativo,
                DataCriacao = DateTime.UtcNow,
                TenantId = tenantId,
            };

            await _context.Clientes.AddAsync(cliente);

            // 6. Criar Local Exemplo
            var local = new Local
            {
                Id = Guid.NewGuid(),
                Nome = "Quadra Exemplo",
                Tipo = "Futebol",
                Intervalo = 60,
                ValorHora = 100.00m,
                Capacidade = 20,
                Descricao = "Quadra de futebol society",
                Comodidades = new List<string> { "Vestiários", "Estacionamento" },
                Situacao = SituacaoLocal.Ativo,
                Cor = "#FF0000",
                HoraAbertura = "08:00",
                HoraFechamento = "22:00",
                FilialId = filial.Id,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Locais.AddAsync(local);

            // 7. Criar Reserva Exemplo
            var reserva = new Reserva
            {
                Id = Guid.NewGuid(),
                ClienteId = cliente.Id,
                LocalId = local.Id,
                DataInicio = DateTime.UtcNow.Date.AddDays(1).AddHours(10),
                DataFim = DateTime.UtcNow.Date.AddDays(1).AddHours(11),
                Situacao = SituacaoReserva.Confirmada,
                Valor = 100.00m,
                Observacoes = "Reserva teste",
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Reservas.AddAsync(reserva);

            // 8. Criar Recebível Exemplo
            var recebivel = new Recebivel
            {
                Id = Guid.NewGuid(),
                ClienteId = cliente.Id,
                Descricao = "Recebimento teste",
                Valor = 100.00m,
                DataVencimento = DateTime.UtcNow.Date.AddDays(30),
                Situacao = SituacaoRecebivel.Aberto,
                ReservaId = reserva.Id,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };

            await _context.Recebiveis.AddAsync(recebivel);

            await _context.SaveChangesAsync();
        }

        private List<Permissao> CriarPermissoesModulo(string moduloPai, (string nome, string descricao)[] permissoes)
        {
            var lista = new List<Permissao>();
            var tenantId = 1; // Tenant padrão

            foreach (var (nome, descricao) in permissoes)
            {
                var (submodulo, acao) = ParsePermissao(nome);

                lista.Add(new Permissao
                {
                    Id = Guid.NewGuid(),
                    Nome = $"{moduloPai}.{nome}",
                    Descricao = descricao,
                    ModuloPai = moduloPai,
                    Submodulo = submodulo,
                    Acao = acao,
                    Situacao = Domain.Enums.geral.SituacaoBase.Ativo,
                    TenantId = tenantId,
                    DataCriacao = DateTime.UtcNow
                });
            }

            return lista;
        }

        private (string submodulo, string acao) ParsePermissao(string nome)
        {
            if (nome == "acesso")
                return ("", "acesso");

            var partes = nome.Split('.');
            if (partes.Length == 2)
                return (partes[0], partes[1]);

            return ("", nome);
        }
    }
}
