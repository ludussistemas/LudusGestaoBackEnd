using LudusGestao.Domain.Entities;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudusGestao.Infrastructure.Data.Repositories.geral
{
    public class GrupoPermissaoFilialRepository : BaseRepository<GrupoPermissaoFilial>, IGrupoPermissaoFilialRepository
    {
        public GrupoPermissaoFilialRepository(
        ApplicationDbContext context, 
        ITenantService tenantService,
        ITenantFilter<GrupoPermissaoFilial> tenantFilter,
        IQuerySorter<GrupoPermissaoFilial> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies) 
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<GrupoPermissaoFilial?> ObterPorGrupoEFilialAsync(Guid grupoId, Guid filialId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.FirstOrDefaultAsync(g => g.GrupoPermissaoId == grupoId && g.FilialId == filialId);
        }

        public async Task<IEnumerable<GrupoPermissaoFilial>> ObterPorGrupoAsync(Guid grupoId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.GrupoPermissaoId == grupoId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ObterFiliaisPorGrupoAsync(Guid grupoId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.GrupoPermissaoId == grupoId).Select(g => g.FilialId).ToListAsync();
        }

        public async Task<IEnumerable<GrupoPermissaoFilial>> ObterPorFilialAsync(Guid filialId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.FilialId == filialId).ToListAsync();
        }
    }
}
