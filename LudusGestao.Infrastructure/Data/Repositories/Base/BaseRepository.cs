using LudusGestao.Core.Common;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ITenantService _tenantService;
        protected readonly ITenantFilter<T> _tenantFilter;
        protected readonly IQuerySorter<T> _querySorter;
        private readonly FilialFilter<T>? _filialFilter;
        protected readonly IEnumerable<IFilterStrategy> _filterStrategies;

        public BaseRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<T> tenantFilter,
            IQuerySorter<T> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies,
            FilialFilter<T>? filialFilter = null)
        {
            _context = context;
            _tenantService = tenantService;
            _tenantFilter = tenantFilter;
            _querySorter = querySorter;
            _filterStrategies = filterStrategies;
            _filialFilter = filialFilter;
        }

        public virtual async Task<T> ObterPorId(Guid id)
        {
            var query = _context.Set<T>().AsQueryable();
            query = ApplyTenantFilter(query);
            query = ApplyFilialFilter(query);
            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public virtual async Task<(IEnumerable<T> Items, int TotalCount)> ListarPaginado(QueryParamsBase queryParams)
        {
            var query = _context.Set<T>().AsQueryable();

            // Aplicar filtro de tenant automaticamente
            query = ApplyTenantFilter(query);

            // Aplicar filtros
            if (!string.IsNullOrEmpty(queryParams.Filter))
            {
                query = ApplyFilters(query, queryParams.Filter);
            }

            // Aplicar ordenação
            if (!string.IsNullOrEmpty(queryParams.Sort))
            {
                query = _querySorter.Apply(query, queryParams.Sort);
            }

            var totalCount = await query.CountAsync();

            // Paginação
            int skip = queryParams.Start > 0 ? queryParams.Start : (queryParams.Page - 1) * queryParams.Limit;
            var items = await query.Skip(skip).Take(queryParams.Limit).ToListAsync();
            return (items, totalCount);
        }

        protected virtual IQueryable<T> ApplyFilters(IQueryable<T> query, string filter)
        {
            foreach (var strategy in _filterStrategies)
            {
                if (strategy.CanHandle(filter))
                {
                    return strategy.Apply(query, filter);
                }
            }
            return query;
        }



        public virtual async Task Criar(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual Task Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = await ObterPorId(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> Listar()
        {
            var query = _context.Set<T>().AsQueryable();
            query = ApplyTenantFilter(query);
            query = ApplyFilialFilter(query);
            return await query.ToListAsync();
        }

        protected virtual IQueryable<T> ApplyTenantFilter(IQueryable<T> query)
        {
            try
            {
                var tenantId = _tenantService.GetTenantId();
                return _tenantFilter.Apply(query, tenantId);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Tenant não definido ou inválido.", ex);
            }
        }

        protected virtual IQueryable<T> ApplyFilialFilter(IQueryable<T> query)
        {
            if (_filialFilter != null)
            {
                return _filialFilter.Apply(query);
            }
            return query;
        }
    }
}
