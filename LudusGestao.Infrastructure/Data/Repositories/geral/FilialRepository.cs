// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Domain.Entities;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

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