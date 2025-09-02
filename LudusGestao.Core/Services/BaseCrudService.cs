using AutoMapper;
using LudusGestao.Core.Common;
using LudusGestao.Core.Interfaces.Infrastructure;
using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Models;

namespace LudusGestao.Core.Services
{
    public class BaseCrudService<TEntity, TDto, TCreateDto, TUpdateDto> : IBaseCrudService<TDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseCrudService(IBaseRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<ApiPagedResponse<TDto>> ListarPaginado(QueryParamsBase queryParams)
        {
            var (entities, totalCount) = await _repository.ListarPaginado(queryParams);
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

            if (!string.IsNullOrEmpty(queryParams.Fields))
            {
                var fields = queryParams.Fields.Split(',');
                var filteredDtos = dtos.Select(r =>
                    fields.ToDictionary(f => f, f => r.GetType().GetProperty(f, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.GetValue(r, null))
                ).Cast<TDto>().ToList();
                return new ApiPagedResponse<TDto>(filteredDtos, queryParams.Page, queryParams.Limit, totalCount);
            }

            return new ApiPagedResponse<TDto>(dtos, queryParams.Page, queryParams.Limit, totalCount);
        }

        public virtual async Task<IEnumerable<TDto>> Listar()
        {
            var entities = await _repository.Listar();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> ObterPorId(Guid id)
        {
            var entity = await _repository.ObterPorId(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Criar(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.Criar(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Atualizar(Guid id, TUpdateDto dto)
        {
            var entity = await _repository.ObterPorId(id);
            if (entity == null) return default;
            _mapper.Map(dto, entity);
            await _repository.Atualizar(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> Remover(Guid id)
        {
            var entity = await _repository.ObterPorId(id);
            if (entity == null) return false;
            await _repository.Remover(id);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
