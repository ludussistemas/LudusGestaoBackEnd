using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral.permissao
{
    public class ModuloRepository : BaseRepository<Modulo>, IModuloRepository
    {
        public ModuloRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<Modulo> tenantFilter,
            IQuerySorter<Modulo> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies)
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<Modulo?> ObterPorNomeAsync(string nome)
        {
            return await _context.Set<Modulo>()
                .FirstOrDefaultAsync(m => m.Nome == nome);
        }
    }
}
