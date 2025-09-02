using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services.infra;

namespace LudusGestao.Application.Services
{
    public class TenantCreationService : ITenantCreationService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public TenantCreationService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<int> GenerateNewTenantIdAsync()
        {
            var maxTenantId = await _empresaRepository.GetMaxTenantIdAsync();
            return maxTenantId + 1;
        }
    }
}
