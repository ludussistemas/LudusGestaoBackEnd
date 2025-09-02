using AutoMapper;
using LudusGestao.Application.DTOs.geral.Permissao;
using LudusGestao.Core.Common;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Interfaces.Repositories.geral;

namespace LudusGestao.Application.Services
{
    public class PermissaoService : IBaseCrudService<PermissaoDTO, CreatePermissaoDTO, UpdatePermissaoDTO>
    {
        private readonly IPermissaoRepository _repository;
        private readonly IMapper _mapper;

        public PermissaoService(IPermissaoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PermissaoDTO> ObterPorId(Guid id)
        {
            var entity = await _repository.ObterPorId(id);
            return _mapper.Map<PermissaoDTO>(entity);
        }

        public async Task<IEnumerable<PermissaoDTO>> Listar()
        {
            var entities = await _repository.Listar();
            return _mapper.Map<IEnumerable<PermissaoDTO>>(entities);
        }

        public async Task<ApiPagedResponse<PermissaoDTO>> ListarPaginado(QueryParamsBase queryParams)
        {
            var (items, totalCount) = await _repository.ListarPaginado(queryParams);
            var dtos = _mapper.Map<IEnumerable<PermissaoDTO>>(items);

            return new ApiPagedResponse<PermissaoDTO>(dtos, queryParams.Page, queryParams.Limit, totalCount);
        }

        public async Task<IEnumerable<PermissaoDTO>> ObterPorModuloPai(string moduloPai)
        {
            var entities = await _repository.ObterPorModuloPai(moduloPai);
            return _mapper.Map<IEnumerable<PermissaoDTO>>(entities);
        }

        public async Task<IEnumerable<PermissaoDTO>> ObterPorSubmodulo(string submodulo)
        {
            var entities = await _repository.ObterPorSubmodulo(submodulo);
            return _mapper.Map<IEnumerable<PermissaoDTO>>(entities);
        }

        public async Task<IEnumerable<string>> ObterModulosPai()
        {
            return await _repository.ObterModulosPai();
        }

        public async Task<IEnumerable<string>> ObterSubmodulos()
        {
            return await _repository.ObterSubmodulos();
        }

        // Métodos não implementados para este serviço
        public Task<PermissaoDTO> Criar(CreatePermissaoDTO dto) => throw new NotImplementedException();
        public Task<PermissaoDTO> Atualizar(Guid id, UpdatePermissaoDTO dto) => throw new NotImplementedException();
        public Task<bool> Remover(Guid id) => throw new NotImplementedException();
    }
}