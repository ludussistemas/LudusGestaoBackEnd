using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using LudusGestao.Application.Services;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Application.Tests.Services
{
    public class PermissaoVerificacaoServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IGrupoPermissaoRepository> _grupoRepositoryMock;
        private readonly Mock<IPermissaoRepository> _permissaoRepositoryMock;
        private readonly Mock<IGrupoPermissaoFilialRepository> _grupoFilialRepositoryMock;
        private readonly Mock<IUsuarioPermissaoFilialRepository> _usuarioFilialRepositoryMock;
        private readonly PermissaoVerificacaoService _service;

        public PermissaoVerificacaoServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _grupoRepositoryMock = new Mock<IGrupoPermissaoRepository>();
            _permissaoRepositoryMock = new Mock<IPermissaoRepository>();
            _grupoFilialRepositoryMock = new Mock<IGrupoPermissaoFilialRepository>();
            _usuarioFilialRepositoryMock = new Mock<IUsuarioPermissaoFilialRepository>();

            _service = new PermissaoVerificacaoService(
                _usuarioRepositoryMock.Object,
                _grupoRepositoryMock.Object,
                _permissaoRepositoryMock.Object,
                _grupoFilialRepositoryMock.Object,
                _usuarioFilialRepositoryMock.Object);
        }

        [Fact]
        public async Task VerificarPermissaoUsuarioAsync_UsuarioNaoExiste_DeveRetornarFalse()
        {
            // Arrange
            var usuarioId = Guid.NewGuid();
            var permissao = "empresa.visualizar";
            
            _usuarioRepositoryMock.Setup(x => x.ObterPorId(usuarioId))
                .ReturnsAsync((Usuario)null);

            // Act
            var resultado = await _service.VerificarPermissaoUsuarioAsync(usuarioId, permissao);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task VerificarPermissaoUsuarioAsync_UsuarioComPermissao_DeveRetornarTrue()
        {
            // Arrange
            var usuarioId = Guid.NewGuid();
            var permissao = "empresa.visualizar";
            var usuario = new Usuario { Id = usuarioId, GrupoPermissaoId = Guid.NewGuid() };
            
            _usuarioRepositoryMock.Setup(x => x.ObterPorId(usuarioId))
                .ReturnsAsync(usuario);
            
            _grupoRepositoryMock.Setup(x => x.ObterPorId(usuario.GrupoPermissaoId.Value))
                .ReturnsAsync(new GrupoPermissao { Id = usuario.GrupoPermissaoId.Value });
            
            _permissaoRepositoryMock.Setup(x => x.ObterPermissoesPorGrupoAsync(usuario.GrupoPermissaoId.Value))
                .ReturnsAsync(new List<Permissao> { new Permissao { Nome = permissao } });

            // Act
            var resultado = await _service.VerificarPermissaoUsuarioAsync(usuarioId, permissao);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ObterPermissoesUsuarioAsync_UsuarioSemGrupo_DeveRetornarPermissoesIndividuais()
        {
            // Arrange
            var usuarioId = Guid.NewGuid();
            var usuario = new Usuario { Id = usuarioId, GrupoPermissaoId = null };
            
            _usuarioRepositoryMock.Setup(x => x.ObterPorId(usuarioId))
                .ReturnsAsync(usuario);
            
            _usuarioFilialRepositoryMock.Setup(x => x.ObterPermissoesPorUsuarioAsync(usuarioId))
                .ReturnsAsync(new List<UsuarioPermissaoFilial>());

            // Act
            var resultado = await _service.ObterPermissoesUsuarioAsync(usuarioId);

            // Assert
            Assert.Empty(resultado);
        }
    }
}
