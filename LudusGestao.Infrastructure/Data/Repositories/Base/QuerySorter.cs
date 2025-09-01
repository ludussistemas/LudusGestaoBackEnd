using System.Linq;
using Microsoft.EntityFrameworkCore;
using LudusGestao.Domain.Interfaces.Repositories.Base;

namespace LudusGestao.Infrastructure.Data.Repositories.Base
{
    public class QuerySorter<T> : IQuerySorter<T>
    {
        public IQueryable<T> Apply(IQueryable<T> query, string sort)
        {
            if (string.IsNullOrEmpty(sort)) return query;

            var sortFields = sort.Split(',');
            var isFirst = true;

            foreach (var sortField in sortFields)
            {
                var trimmedField = sortField.Trim();
                var isDescending = trimmedField.StartsWith("-");
                var actualField = isDescending ? trimmedField.Substring(1) : trimmedField;

                var fieldMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "nome", "Nome" },
                    { "email", "Email" },
                    { "documento", "Documento" },
                    { "data", "Data" },
                    { "dataCriacao", "DataCriacao" },
                    { "dataVencimento", "DataVencimento" },
                    { "valor", "Valor" },
                    { "situacao", "Situacao" }
                };

                var mappedField = fieldMappings.ContainsKey(actualField) ? fieldMappings[actualField] : actualField;

                try
                {
                    if (isFirst)
                    {
                        query = isDescending ? query.OrderByDescending(e => EF.Property<object>(e, mappedField)) 
                                            : query.OrderBy(e => EF.Property<object>(e, mappedField));
                        isFirst = false;
                    }
                    else
                    {
                        query = isDescending ? query.OrderByDescending(e => EF.Property<object>(e, mappedField)) 
                                            : query.OrderBy(e => EF.Property<object>(e, mappedField));
                    }
                }
                catch
                {
                    // Campo não existe, ignorar
                }
            }

            return query;
        }
    }
}
