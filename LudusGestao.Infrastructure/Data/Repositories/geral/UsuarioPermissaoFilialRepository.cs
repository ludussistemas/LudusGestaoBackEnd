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
    public class UsuarioPermissaoFilialRepository : BaseRepository<UsuarioPermissaoFilial>, IUsuarioPermissaoFilialRepository
    {
        public UsuarioPermissaoFilialRepository(
        ApplicationDbContext context, 
        ITenantService tenantService,
        ITenantFilter<UsuarioPermissaoFilial> tenantFilter,
        IQuerySorter<UsuarioPermissaoFilial> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies) 
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<UsuarioPermissaoFilial?> ObterPorUsuarioEFilialAsync(Guid usuarioId, Guid filialId)
        {
            var query = _context.Set<UsuarioPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.FirstOrDefaultAsync(u => u.UsuarioId == usuarioId && u.FilialId == filialId);
        }

        public async Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            var query = _context.Set<UsuarioPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(u => u.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ObterFiliaisPorUsuarioAsync(Guid usuarioId)
        {
            var query = _context.Set<UsuarioPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(u => u.UsuarioId == usuarioId).Select(u => u.FilialId).ToListAsync();
        }

        public async Task<IEnumerable<UsuarioPermissaoFilial>> ObterPorFilialAsync(Guid filialId)
        {
            var query = _context.Set<UsuarioPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(u => u.FilialId == filialId).ToListAsync();
        }
    }
}
