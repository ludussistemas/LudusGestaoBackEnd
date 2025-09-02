using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Controllers
{
    [ApiController]
    public abstract class SimpleBaseCrudController<TService, TDto, TCreateDto, TUpdateDto> : BaseCrudController<TService, TDto, TCreateDto, TUpdateDto>
        where TService : class, IBaseCrudService<TDto, TCreateDto, TUpdateDto>
    {
        protected SimpleBaseCrudController(TService service) : base(
            service,
            new DefaultValidationService<TDto, TCreateDto, TUpdateDto>(),
            new DefaultLoggingService(),
            new DefaultAuthorizationService())
        {
        }
    }
}
