using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LudusGestao.Domain.Entities.Base;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Domain.Common;
using LudusGestao.Domain.Interfaces.Services;
using System.Linq;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;

namespace LudusGestao.Infrastructure.Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ITenantService _tenantService;
        protected readonly ITenantFilter<T> _tenantFilter;
        protected readonly IQuerySorter<T> _querySorter;
        protected readonly IEnumerable<IFilterStrategy> _filterStrategies;

        public BaseRepository(
            ApplicationDbContext context, 
            ITenantService tenantService,
            ITenantFilter<T> tenantFilter,
            IQuerySorter<T> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies)
        {
            _context = context;
            _tenantService = tenantService;
            _tenantFilter = tenantFilter;
            _querySorter = querySorter;
            _filterStrategies = filterStrategies;
        }

        public virtual async Task<T> ObterPorId(Guid id)
        {
            var query = _context.Set<T>().AsQueryable();
            query = ApplyTenantFilter(query);
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
            try
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
            catch
            {
                return query;
            }
        }



        public virtual async Task Criar(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.DataCriacao = DateTime.UtcNow;
                baseEntity.DataAtualizacao = null;
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Atualizar(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.DataAtualizacao = DateTime.UtcNow;
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = await ObterPorId(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> Listar()
        {
            var query = _context.Set<T>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.ToListAsync();
        }

        protected virtual IQueryable<T> ApplyTenantFilter(IQueryable<T> query)
        {
            try
            {
                var tenantId = _tenantService.GetTenantId();
                return _tenantFilter.Apply(query, tenantId);
            }
            catch
            {
                return query.Where(x => false);
            }
        }
    }
} 