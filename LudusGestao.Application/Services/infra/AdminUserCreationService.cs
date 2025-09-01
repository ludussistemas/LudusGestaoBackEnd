using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Application.DTOs.Gerencialmento;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.geral;
using LudusGestao.Domain.Common.Constants;

namespace LudusGestao.Application.Services
{
    public class AdminUserCreationService : IAdminUserCreationService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AdminUserCreationService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CreateAdminUserAsync(string nome, string email, string senha, Guid companyId, int tenantId)
        {
            var senhaPadrao = UserConstants.DefaultAdminPassword;
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senhaPadrao);
            var emailUsuario = $"administrador@{nome.Replace(" ", "").ToLower()}.com.br";
            
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = UserConstants.DefaultAdminName,
                Email = emailUsuario,
                Telefone = "",
                Cargo = UserConstants.DefaultAdminRole,
                EmpresaId = companyId,
                GrupoPermissaoId = null,
                Situacao = SituacaoBase.Ativo,
                UltimoAcesso = DateTime.UtcNow,
                Foto = "",
                Senha = senhaHash,
                TenantId = tenantId,
            };

            await _usuarioRepository.Criar(usuario);
            return usuario;
        }
    }
}
