using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral.permissao
{
    public class GrupoPermissaoSubmoduloAcaoRepository : BaseRepository<GrupoPermissaoSubmoduloAcao>, IGrupoPermissaoSubmoduloAcaoRepository
    {
        public GrupoPermissaoSubmoduloAcaoRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<GrupoPermissaoSubmoduloAcao> tenantFilter,
            IQuerySorter<GrupoPermissaoSubmoduloAcao> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<bool> TemPermissaoSubmoduloAsync(List<Guid> grupoIds, string submoduloNome, string acaoNome)
        {
            return await _context.GruposPermissoesSubmodulosAcoes
                .Include(gpsa => gpsa.Submodulo)
                .Include(gpsa => gpsa.Acao)
                .AnyAsync(gpsa => grupoIds.Contains(gpsa.GrupoId) && 
                                 gpsa.Submodulo.Nome == submoduloNome && 
                                 gpsa.Acao.Nome == acaoNome && 
                                 gpsa.Permitido);
        }

        public async Task<IEnumerable<Submodulo>> ObterSubmodulosPermitidosAsync(List<Guid> grupoIds, Guid moduloId)
        {
            return await _context.GruposPermissoesSubmodulosAcoes
                .Include(gpsa => gpsa.Submodulo)
                .Where(gpsa => grupoIds.Contains(gpsa.GrupoId) && 
                              gpsa.Submodulo.ModuloId == moduloId && 
                              gpsa.Permitido)
                .Select(gpsa => gpsa.Submodulo)
                .Distinct()
                .ToListAsync();
        }
    }
}


