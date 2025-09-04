// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.evento.Local;
using LudusGestao.Domain.Enums.eventos;
using System;
using System.Collections.Generic;

public class CreateLocalDTO
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public int Intervalo { get; set; }
    public decimal ValorHora { get; set; }
    public int? Capacidade { get; set; }
    public string Descricao { get; set; }
    public List<string> Comodidades { get; set; }
    public SituacaoLocal Situacao { get; set; }
    public string Cor { get; set; }
    public string HoraAbertura { get; set; }
    public string HoraFechamento { get; set; }
    public Guid FilialId { get; set; }
}
