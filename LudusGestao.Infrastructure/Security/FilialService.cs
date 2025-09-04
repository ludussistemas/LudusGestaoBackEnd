using LudusGestao.Domain.Interfaces.Services.infra;

namespace LudusGestao.Infrastructure.Security
{
    public class CurrentFilialService : IFilialService
    {
        private static readonly AsyncLocal<Guid?> _filialId = new AsyncLocal<Guid?>();

        public void SetFilialId(Guid filialId)
        {
            _filialId.Value = filialId;
        }

        public Guid GetFilialId()
        {
            if (!_filialId.Value.HasValue)
                throw new InvalidOperationException("Filial não definida");
            return _filialId.Value.Value;
        }

        public bool TryGetFilialId(out Guid filialId)
        {
            if (_filialId.Value.HasValue)
            {
                filialId = _filialId.Value.Value;
                return true;
            }
            filialId = Guid.Empty;
            return false;
        }
    }
}


