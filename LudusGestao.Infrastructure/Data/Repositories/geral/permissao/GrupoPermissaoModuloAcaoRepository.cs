using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral.permissao
{
    public class GrupoPermissaoModuloAcaoRepository : BaseRepository<GrupoPermissaoModuloAcao>, IGrupoPermissaoModuloAcaoRepository
    {
        public GrupoPermissaoModuloAcaoRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<GrupoPermissaoModuloAcao> tenantFilter,
            IQuerySorter<GrupoPermissaoModuloAcao> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<bool> TemPermissaoModuloAsync(List<Guid> grupoIds, string moduloNome, string acaoNome)
        {
            return await _context.GruposPermissoesModulosAcoes
                .Include(gpma => gpma.Modulo)
                .Include(gpma => gpma.Acao)
                .AnyAsync(gpma => grupoIds.Contains(gpma.GrupoId) && 
                                 gpma.Modulo.Nome == moduloNome && 
                                 gpma.Acao.Nome == acaoNome && 
                                 gpma.Permitido);
        }

        public async Task<IEnumerable<Modulo>> ObterModulosPermitidosAsync(List<Guid> grupoIds)
        {
            return await _context.GruposPermissoesModulosAcoes
                .Include(gpma => gpma.Modulo)
                .Where(gpma => grupoIds.Contains(gpma.GrupoId) && gpma.Permitido)
                .Select(gpma => gpma.Modulo)
                .Distinct()
                .ToListAsync();
        }
    }
}


