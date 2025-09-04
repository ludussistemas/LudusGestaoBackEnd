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
    public class AcaoRepository : BaseRepository<Acao>, IAcaoRepository
    {
        public AcaoRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<Acao> tenantFilter,
            IQuerySorter<Acao> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<Acao?> ObterPorNomeAsync(string nome)
        {
            return await _context.Acoes
                .FirstOrDefaultAsync(a => a.Nome == nome);
        }
    }
}


