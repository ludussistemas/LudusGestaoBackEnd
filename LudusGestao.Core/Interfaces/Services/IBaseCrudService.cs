using LudusGestao.Core.Common;
using LudusGestao.Core.Models;

namespace LudusGestao.Core.Interfaces.Services
{
    public interface IBaseCrudService<TDto, TCreateDto, TUpdateDto>
    {
        Task<ApiPagedResponse<TDto>> ListarPaginado(QueryParamsBase queryParams);
        Task<IEnumerable<TDto>> Listar();
        Task<TDto> ObterPorId(Guid id);
        Task<TDto> Criar(TCreateDto dto);
        Task<TDto> Atualizar(Guid id, TUpdateDto dto);
        Task<bool> Remover(Guid id);
    }
}
