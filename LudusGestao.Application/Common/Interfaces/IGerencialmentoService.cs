using LudusGestao.Application.DTOs.Gerencialmento;
using LudusGestao.Core.Models;

namespace LudusGestao.Application.Common.Interfaces
{
    public interface IGerencialmentoService
    {
        Task<ApiResponse<NovoClienteResultadoDTO>> CriarNovoCliente(CriarNovoClienteDTO dto);
        Task<ApiResponse<object>> AlterarSenha(AlterarSenhaDTO dto);
    }
}