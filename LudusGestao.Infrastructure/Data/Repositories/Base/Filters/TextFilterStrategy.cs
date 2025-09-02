namespace LudusGestao.Infrastructure.Data.Repositories.Base.Filters
{
    public class TextFilterStrategy : IFilterStrategy
    {
        public bool CanHandle(string filter)
        {
            return !filter.Contains(":");
        }

        public IQueryable<T> Apply<T>(IQueryable<T> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var textFields = new[] { "Nome", "Email", "Documento", "Telefone", "Observacoes", "Esporte" };

            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
            var searchConstant = System.Linq.Expressions.Expression.Constant(searchTerm.ToLower());
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            var conditions = new List<System.Linq.Expressions.Expression>();

            foreach (var field in textFields)
            {
                try
                {
                    var property = System.Linq.Expressions.Expression.Property(parameter, field);
                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                    var toLowerCall = System.Linq.Expressions.Expression.Call(property, toLowerMethod);
                    var containsCall = System.Linq.Expressions.Expression.Call(toLowerCall, containsMethod, searchConstant);
                    conditions.Add(containsCall);
                }
                catch
                {
                    // Campo não existe na entidade, ignorar
                }
            }

            if (conditions.Any())
            {
                var combinedCondition = conditions.Aggregate((a, b) =>
                    System.Linq.Expressions.Expression.Or(a, b));
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(combinedCondition, parameter);
                return query.Where(lambda);
            }

            return query;
        }
    }
}
