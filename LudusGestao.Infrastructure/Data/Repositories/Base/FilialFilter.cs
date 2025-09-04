using LudusGestao.Core.Entities.Base;
using LudusGestao.Domain.Interfaces.Services.infra;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.Base
{
    public class FilialFilter<T>
    {
        private readonly IFilialService _filialService;

        public FilialFilter(IFilialService filialService)
        {
            _filialService = filialService;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (typeof(IFilialEntity).IsAssignableFrom(typeof(T)))
            {
                if (_filialService.TryGetFilialId(out var filialId))
                {
                    return query.Where(e => EF.Property<Guid>(e, "FilialId") == filialId);
                }
                // Se não houver filial no contexto, não filtra
            }
            return query;
        }
    }
}


