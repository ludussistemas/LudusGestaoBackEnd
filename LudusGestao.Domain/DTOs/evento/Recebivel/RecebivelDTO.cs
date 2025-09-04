// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.evento.Recebivel;
using LudusGestao.Domain.DTOs.reserva.Cliente;
using LudusGestao.Domain.DTOs.reserva.Reservas;
using LudusGestao.Domain.Enums.eventos;
using System;

public class RecebivelDTO
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Associações
    public Guid ClienteId { get; set; }
    public ClienteDTO Cliente { get; set; }
    public Guid? ReservaId { get; set; }
    public ReservaDTO Reserva { get; set; }

    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public SituacaoRecebivel Situacao { get; set; }
    public int TenantId { get; set; }
}
