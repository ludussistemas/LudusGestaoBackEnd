namespace LudusGestao.Domain.Enums.eventos
{
    /// <summary>
    /// Situações para reservas
    /// </summary>
    public enum SituacaoReserva
    {
        Pendente = 1,      // Reserva criada, aguardando confirmação
        Confirmada = 2,    // Reserva confirmada
        Cancelada = 3,     // Reserva cancelada
        Finalizada = 4,    // Evento realizado
        Expirada = 5       // Data passou sem confirmação
    }
}
