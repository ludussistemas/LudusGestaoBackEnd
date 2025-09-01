using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace LudusGestao.Infrastructure.Data.Repositories.Base.Filters
{
    public class JsonFilterStrategy : IFilterStrategy
    {
        public bool CanHandle(string filter)
        {
            return filter.StartsWith("{") && filter.EndsWith("}");
        }

        public IQueryable<T> Apply<T>(IQueryable<T> query, string jsonFilter)
        {
            try
            {
                var filters = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonFilter);
                foreach (var filter in filters)
                {
                    query = ApplySimpleFilter(query, filter.Key, filter.Value?.ToString());
                }
                return query;
            }
            catch
            {
                return query;
            }
        }

        private IQueryable<T> ApplySimpleFilter<T>(IQueryable<T> query, string field, string value)
        {
            if (string.IsNullOrEmpty(value)) return query;

            var fieldMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "nome", "Nome" },
                { "email", "Email" },
                { "documento", "Documento" },
                { "telefone", "Telefone" },
                { "situacao", "Situacao" },
                { "data", "Data" },
                { "dataCriacao", "DataCriacao" },
                { "valor", "Valor" },
                { "esporte", "Esporte" },
                { "cor", "Cor" }
            };

            var actualField = fieldMappings.ContainsKey(field) ? fieldMappings[field] : field;

            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
            var property = System.Linq.Expressions.Expression.Property(parameter, actualField);
            var constant = System.Linq.Expressions.Expression.Constant(value);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            
            if (containsMethod != null)
            {
                var containsCall = System.Linq.Expressions.Expression.Call(property, containsMethod, constant);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(containsCall, parameter);
                return query.Where(lambda);
            }

            return query;
        }
    }
}
