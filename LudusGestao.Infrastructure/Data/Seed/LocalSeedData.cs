using LudusGestao.Domain.Entities.eventos;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Enums.eventos;
using LudusGestao.Domain.Enums.geral;

namespace LudusGestao.Infrastructure.Data.Seed
{
    public static class LocalSeedData
    {
        public static Local GetLocalExemplo(Guid filialId, int tenantId)
        {
            return new Local
            {
                Id = Guid.NewGuid(),
                Nome = "Quadra Exemplo",
                Tipo = "Futebol",
                Intervalo = 60,
                ValorHora = 100.00m,
                Capacidade = 20,
                Descricao = "Quadra de futebol society",
                Comodidades = new List<string> { "Vestiários", "Estacionamento" },
                Situacao = SituacaoLocal.Ativo,
                Cor = "#FF0000",
                HoraAbertura = "08:00",
                HoraFechamento = "22:00",
                FilialId = filialId,
                TenantId = tenantId,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
