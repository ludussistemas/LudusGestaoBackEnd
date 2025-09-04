using LudusGestao.Domain.Entities.geral.permissao;
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

                var tenantId = 1;

                // 1. Seed de módulos, submódulos e ações (novo esquema)
                await SeedNovasPermissoesAsync();

                // 2. Seed de grupos de permissões
                var grupos = await SeedGruposAsync(tenantId);

                // 3. Seed de dados básicos (empresa, filial, usuário admin)
                await SeedDadosBasicosAsync(grupos, tenantId);

                return true;
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro no seed: {ex.Message}");
                return false;
            }
        }

        private async Task SeedNovasPermissoesAsync()
        {
            // Seed de módulos
            if (!await _context.Modulos.AnyAsync())
            {
                var modulos = PermissaoSeedData.GetModulos();
                await _context.Modulos.AddRangeAsync(modulos);
                await _context.SaveChangesAsync();
            }

            // Seed de submódulos
            if (!await _context.Submodulos.AnyAsync())
            {
                var moduloEventos = await _context.Modulos.FirstOrDefaultAsync(m => m.Nome == "Eventos");
                var moduloConfiguracoes = await _context.Modulos.FirstOrDefaultAsync(m => m.Nome == "Configuracoes");
                
                if (moduloEventos != null && moduloConfiguracoes != null)
                {
                    var submodulos = PermissaoSeedData.GetSubmodulos(moduloEventos.Id, moduloConfiguracoes.Id);
                    await _context.Submodulos.AddRangeAsync(submodulos);
                    await _context.SaveChangesAsync();
                }
            }

            // Seed de ações
            if (!await _context.Acoes.AnyAsync())
            {
                var acoes = PermissaoSeedData.GetAcoes();
                await _context.Acoes.AddRangeAsync(acoes);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<List<GrupoPermissao>> SeedGruposAsync(int tenantId)
        {
            if (await _context.GruposPermissoes.AnyAsync())
            {
                return await _context.GruposPermissoes.ToListAsync();
            }

            var grupos = GrupoPermissaoSeedData.GetGrupos(tenantId);
            await _context.GruposPermissoes.AddRangeAsync(grupos);
            await _context.SaveChangesAsync();

            return grupos;
        }

        private async Task SeedDadosBasicosAsync(List<GrupoPermissao> grupos, int tenantId)
        {
            // 1. Criar Empresa
            var empresa = EmpresaSeedData.GetEmpresa(tenantId);
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            // 2. Criar Filial
            var filial = FilialSeedData.GetFilial(empresa.Id, tenantId);
            await _context.Filiais.AddAsync(filial);
            await _context.SaveChangesAsync();

            // 3. Obter grupo administrador
            var grupoAdmin = grupos.FirstOrDefault(g => g.Nome == "Administrador");

            // 4. Criar Usuário Administrador
            var usuarioAdmin = UsuarioSeedData.GetUsuarioAdmin(empresa.Id, grupoAdmin?.Id, tenantId);
            await _context.Usuarios.AddAsync(usuarioAdmin);
            await _context.SaveChangesAsync();

            // 5. Criar Cliente Exemplo
            var cliente = ClienteSeedData.GetClienteExemplo(filial.Id, tenantId);
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // 6. Criar Local Exemplo
            var local = LocalSeedData.GetLocalExemplo(filial.Id, tenantId);
            await _context.Locais.AddAsync(local);
            await _context.SaveChangesAsync();

            // 7. Criar Reserva Exemplo
            var reserva = ReservaSeedData.GetReservaExemplo(cliente.Id, local.Id, filial.Id, tenantId);
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();

            // 8. Criar Recebível Exemplo
            var recebivel = RecebivelSeedData.GetRecebivelExemplo(cliente.Id, reserva.Id, filial.Id, tenantId);
            await _context.Recebiveis.AddAsync(recebivel);
            await _context.SaveChangesAsync();

            // 9. Seed relacionamentos de permissões e associação usuário-filial-grupo
            if (grupoAdmin != null)
            {
                await SeedRelacionamentosPermissoesAsync(grupoAdmin, filial.Id, usuarioAdmin.Id);
            }
        }

        private async Task SeedRelacionamentosPermissoesAsync(GrupoPermissao grupoAdmin, Guid filialId, Guid usuarioAdminId)
        {
            // Evitar duplicidade: só semear se ainda não houver relações
            var jaTemModuloAcoes = await _context.GruposPermissoesModulosAcoes.AnyAsync(g => g.GrupoId == grupoAdmin.Id);
            var jaTemSubmoduloAcoes = await _context.GruposPermissoesSubmodulosAcoes.AnyAsync(g => g.GrupoId == grupoAdmin.Id);
            var jaTemUsuarioFilialGrupo = await _context.UsuariosFiliaisGrupos.AnyAsync(u => u.UsuarioId == usuarioAdminId && u.FilialId == filialId);

            // Buscar ações necessárias
            var acoes = await _context.Acoes.ToListAsync();
            var acaoAcesso = acoes.FirstOrDefault(a => a.Nome == "Acesso")?.Id;
            var acaoVisualizar = acoes.FirstOrDefault(a => a.Nome == "Visualizar")?.Id;
            var acaoCriar = acoes.FirstOrDefault(a => a.Nome == "Criar")?.Id;
            var acaoEditar = acoes.FirstOrDefault(a => a.Nome == "Editar")?.Id;
            var acaoExcluir = acoes.FirstOrDefault(a => a.Nome == "Excluir")?.Id;

            // Garantir que tenhamos ao menos Visualizar para funcionamento básico
            if (acaoVisualizar == null)
                return;

            // Conceder permissões em Módulos (Visualizar)
            if (!jaTemModuloAcoes)
            {
                var modulos = await _context.Modulos.ToListAsync();
                var entradasModulo = new List<GrupoPermissaoModuloAcao>();
                foreach (var modulo in modulos)
                {
                    entradasModulo.Add(new GrupoPermissaoModuloAcao
                    {
                        Id = Guid.NewGuid(),
                        GrupoId = grupoAdmin.Id,
                        ModuloId = modulo.Id,
                        AcaoId = acaoVisualizar.Value,
                        DataCriacao = DateTime.UtcNow
                    });
                }
                // Também conceder Acesso se existir
                if (acaoAcesso.HasValue)
                {
                    foreach (var modulo in modulos)
                    {
                        entradasModulo.Add(new GrupoPermissaoModuloAcao
                        {
                            Id = Guid.NewGuid(),
                            GrupoId = grupoAdmin.Id,
                            ModuloId = modulo.Id,
                            AcaoId = acaoAcesso.Value,
                            DataCriacao = DateTime.UtcNow
                        });
                    }
                }
                await _context.GruposPermissoesModulosAcoes.AddRangeAsync(entradasModulo);
                await _context.SaveChangesAsync();
            }

            // Conceder permissões em Submódulos (CRUD + Visualizar, se ações existirem)
            if (!jaTemSubmoduloAcoes)
            {
                var submodulos = await _context.Submodulos.ToListAsync();
                var entradasSubmodulo = new List<GrupoPermissaoSubmoduloAcao>();
                foreach (var sub in submodulos)
                {
                    // Visualizar sempre
                    entradasSubmodulo.Add(new GrupoPermissaoSubmoduloAcao
                    {
                        Id = Guid.NewGuid(),
                        GrupoId = grupoAdmin.Id,
                        SubmoduloId = sub.Id,
                        AcaoId = acaoVisualizar!.Value,
                        DataCriacao = DateTime.UtcNow
                    });
                    // CRUD quando disponível
                    if (acaoCriar.HasValue)
                        entradasSubmodulo.Add(new GrupoPermissaoSubmoduloAcao { Id = Guid.NewGuid(), GrupoId = grupoAdmin.Id, SubmoduloId = sub.Id, AcaoId = acaoCriar.Value, DataCriacao = DateTime.UtcNow });
                    if (acaoEditar.HasValue)
                        entradasSubmodulo.Add(new GrupoPermissaoSubmoduloAcao { Id = Guid.NewGuid(), GrupoId = grupoAdmin.Id, SubmoduloId = sub.Id, AcaoId = acaoEditar.Value, DataCriacao = DateTime.UtcNow });
                    if (acaoExcluir.HasValue)
                        entradasSubmodulo.Add(new GrupoPermissaoSubmoduloAcao { Id = Guid.NewGuid(), GrupoId = grupoAdmin.Id, SubmoduloId = sub.Id, AcaoId = acaoExcluir.Value, DataCriacao = DateTime.UtcNow });
                }
                await _context.GruposPermissoesSubmodulosAcoes.AddRangeAsync(entradasSubmodulo);
                await _context.SaveChangesAsync();
            }

            // Associar usuário admin ao grupo e filial
            if (!jaTemUsuarioFilialGrupo)
            {
                var assoc = new UsuarioFilialGrupo
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuarioAdminId,
                    FilialId = filialId,
                    GrupoId = grupoAdmin.Id,
                    DataCriacao = DateTime.UtcNow
                };
                await _context.UsuariosFiliaisGrupos.AddAsync(assoc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
