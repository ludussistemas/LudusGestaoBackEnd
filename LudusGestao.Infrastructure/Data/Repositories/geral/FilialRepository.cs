// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;

namespace LudusGestao.Infrastructure.Data.Repositories.geral;

public class FilialRepository : BaseRepository<Filial>, IFilialRepository
{
    public FilialRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<Filial> tenantFilter,
        IQuerySorter<Filial> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }
}
