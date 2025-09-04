using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Enums.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral.permissao
{
    public class GrupoPermissaoRepository : BaseRepository<GrupoPermissao>, IGrupoPermissaoRepository
    {
        public GrupoPermissaoRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<GrupoPermissao> tenantFilter,
            IQuerySorter<GrupoPermissao> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<IEnumerable<GrupoPermissao>> ObterPorNomeAsync(string nome)
        {
            var query = _context.Set<GrupoPermissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.Nome == nome).ToListAsync();
        }

        public async Task<IEnumerable<GrupoPermissao>> ObterPorSituacaoAsync(SituacaoBase situacao)
        {
            var query = _context.Set<GrupoPermissao>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.Situacao == situacao).ToListAsync();
        }
    }
}
