using LudusGestao.Domain.Enums;
using LudusGestao.Domain.Enums.geral;
using System;

namespace LudusGestao.Application.DTOs.geral.GrupoPermissao
{
    public class GrupoPermissaoDTO
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public SituacaoBase Situacao { get; set; }
        public int TenantId { get; set; }
    }
} 