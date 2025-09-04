using AutoMapper;
using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.DTOs.Empresa;
using LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Domain.DTOs.Gerencialmento;
using LudusGestao.Domain.DTOs.Usuario;
using LudusGestao.Core.Constants;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;

namespace LudusGestao.Application.Services
{
    public class GerencialmentoService : IGerencialmentoService
    {
        private readonly ITenantCreationService _tenantService;
        private readonly ICompanyCreationService _companyService;
        private readonly IBranchCreationService _branchService;
        private readonly IAdminUserCreationService _userService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GerencialmentoService(
            ITenantCreationService tenantService,
            ICompanyCreationService companyService,
            IBranchCreationService branchService,
            IAdminUserCreationService userService,
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _tenantService = tenantService;
            _companyService = companyService;
            _branchService = branchService;
            _userService = userService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NovoClienteResultadoDTO>> CriarNovoCliente(CriarNovoClienteDTO dto)
        {
            try
            {
                var tenantId = await _tenantService.GenerateNewTenantIdAsync();
                var empresa = await _companyService.CreateCompanyAsync(dto.Nome, dto.Cnpj, tenantId);
                var filial = await _branchService.CreateMainBranchAsync(dto.Nome, "001", empresa.Id, tenantId);
                var usuario = await _userService.CreateAdminUserAsync(dto.Nome, "", "", empresa.Id, tenantId);

                return BuildSuccessResponse(empresa, filial, usuario);
            }
            catch (Exception ex)
            {
                return new ApiResponse<NovoClienteResultadoDTO>(default)
                {
                    Success = false,
                    Message = "Erro interno do servidor"
                };
            }
        }

        public async Task<ApiResponse<object>> AlterarSenha(AlterarSenhaDTO dto)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
                if (usuario == null)
                {
                    return new ApiResponse<object>(default)
                    {
                        Success = false,
                        Message = "Usuário não encontrado"
                    };
                }

                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);
                await _usuarioRepository.Atualizar(usuario);

                return new ApiResponse<object>(default, "Senha alterada com sucesso");
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>(default)
                {
                    Success = false,
                    Message = "Erro interno do servidor"
                };
            }
        }

        private ApiResponse<NovoClienteResultadoDTO> BuildSuccessResponse(Empresa empresa, Filial filial, Usuario usuario)
        {
            var empresaRetorno = _mapper.Map<EmpresaDTO>(empresa);
            var filialRetorno = _mapper.Map<FilialDTO>(filial);
            var usuarioRetorno = _mapper.Map<UsuarioDTO>(usuario);

            var resultado = new NovoClienteResultadoDTO
            {
                TenantId = empresa.TenantId,
                Empresa = empresaRetorno,
                FilialMatriz = filialRetorno,
                UsuarioAdmin = usuarioRetorno,
                SenhaPadrao = UserConstants.DefaultAdminPassword
            };

            return new ApiResponse<NovoClienteResultadoDTO>(resultado, "Novo cliente criado com sucesso");
        }
    }
}
