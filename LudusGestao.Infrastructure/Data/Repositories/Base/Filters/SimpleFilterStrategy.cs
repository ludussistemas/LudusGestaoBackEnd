using System.Linq;

namespace LudusGestao.Infrastructure.Data.Repositories.Base.Filters
{
    public class SimpleFilterStrategy : IFilterStrategy
    {
        public bool CanHandle(string filter)
        {
            var parts = filter.Split(':', 2);
            return parts.Length == 2;
        }

        public IQueryable<T> Apply<T>(IQueryable<T> query, string filter)
        {
            var parts = filter.Split(':', 2);
            if (parts.Length != 2) return query;

            var field = parts[0].Trim();
            var value = parts[1].Trim();

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
