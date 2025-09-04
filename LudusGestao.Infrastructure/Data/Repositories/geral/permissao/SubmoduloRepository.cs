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
    public class SubmoduloRepository : BaseRepository<Submodulo>, ISubmoduloRepository
    {
        public SubmoduloRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<Submodulo> tenantFilter,
            IQuerySorter<Submodulo> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<IEnumerable<Submodulo>> ObterPorModuloAsync(Guid moduloId)
        {
            return await _context.Submodulos
                .Where(s => s.ModuloId == moduloId)
                .ToListAsync();
        }
    }
}


