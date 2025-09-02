using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral
{
    public class PermissaoRepository : BaseRepository<Permissao>, IPermissaoRepository
    {
        public PermissaoRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<Permissao> tenantFilter,
        IQuerySorter<Permissao> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<IEnumerable<Permissao>> ObterPorModuloPai(string moduloPai)
        {
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(p => p.ModuloPai == moduloPai).ToListAsync();
        }

        public async Task<IEnumerable<Permissao>> ObterPorSubmodulo(string submodulo)
        {
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(p => p.Submodulo == submodulo).ToListAsync();
        }

        public async Task<IEnumerable<string>> ObterModulosPai()
        {
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Select(p => p.ModuloPai).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> ObterSubmodulos()
        {
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Select(p => p.Submodulo).Distinct().Where(s => !string.IsNullOrEmpty(s)).ToListAsync();
        }

        public async Task<IEnumerable<Permissao>> ObterPorIdsAsync(IEnumerable<Guid> ids)
        {
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Permissao>> ObterPermissoesPorGrupoAsync(Guid grupoId)
        {
            // Implementação básica - retorna todas as permissões do tenant
            // Em uma implementação mais complexa, você poderia ter uma tabela de relacionamento
            var query = _context.Set<Permissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.ToListAsync();
        }
    }
}
