namespace LudusGestao.Domain.Enums.eventos
{
    /// <summary>
    /// Situações para pagáveis
    /// </summary>
    public enum SituacaoPagavel
    {
        Aberto = 1,        // Título ainda não pago
        Vencido = 2,       // Passou da data de vencimento
        Cancelado = 3      // Cancelado
    }
} 