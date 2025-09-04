// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.evento.Recebivel;
using LudusGestao.Domain.Enums.eventos;
using System;

public class CreateRecebivelDTO
{
    public Guid ClienteId { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public SituacaoRecebivel Situacao { get; set; }
    public Guid? ReservaId { get; set; }
}
