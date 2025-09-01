// Arquivo criado para padronização da estrutura de repositórios 
using LudusGestao.Domain.Entities;
using LudusGestao.Domain.Interfaces.Repositories;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Infrastructure.Data.Repositories.eventos;

public class RecebivelRepository : BaseRepository<Recebivel>, IRecebivelRepository
{
    public RecebivelRepository(
        ApplicationDbContext context, 
        ITenantService tenantService,
        ITenantFilter<Recebivel> tenantFilter,
        IQuerySorter<Recebivel> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies) 
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies) { }
} 