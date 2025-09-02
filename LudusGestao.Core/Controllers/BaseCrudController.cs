using LudusGestao.Core.Common;
using LudusGestao.Core.Interfaces.Controllers;
using LudusGestao.Core.Interfaces.Services;
using LudusGestao.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.Core.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TService, TDto, TCreateDto, TUpdateDto> : ControllerBase, IBaseCrudController<TDto, TCreateDto, TUpdateDto, TService>
        where TService : class, IBaseCrudService<TDto, TCreateDto, TUpdateDto>
    {
        protected readonly TService _service;
        protected readonly IValidationService<TDto, TCreateDto, TUpdateDto>? _validationService;
        protected readonly ILoggingService? _loggingService;
        protected readonly IAuthorizationService? _authorizationService;

        // Construtor principal com todas as dependências
        protected BaseCrudController(
            TService service,
            IValidationService<TDto, TCreateDto, TUpdateDto>? validationService = null,
            ILoggingService? loggingService = null,
            IAuthorizationService? authorizationService = null)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _validationService = validationService;
            _loggingService = loggingService;
            _authorizationService = authorizationService;
        }

        // Construtor sobrecarregado apenas com o service
        protected BaseCrudController(TService service) : this(service, null, null, null)
        {
        }

        // Construtor sobrecarregado com service e validação
        protected BaseCrudController(TService service, IValidationService<TDto, TCreateDto, TUpdateDto> validationService)
            : this(service, validationService, null, null)
        {
        }

        // Construtor sobrecarregado com service, validação e logging
        protected BaseCrudController(
            TService service,
            IValidationService<TDto, TCreateDto, TUpdateDto> validationService,
            ILoggingService loggingService)
            : this(service, validationService, loggingService, null)
        {
        }

        public TService Service => _service;

        [HttpGet]
        public virtual async Task<IActionResult> Listar([FromQuery] QueryParamsBase queryParams)
        {
            try
            {
                await LogDebugAsync("Iniciando listagem paginada", new { queryParams });

                if (_authorizationService != null && !await _authorizationService.CanReadAsync())
                {
                    return await _authorizationService.GetUnauthorizedResponseAsync();
                }

                var paged = await _service.ListarPaginado(queryParams);

                await LogInformationAsync("Listagem paginada realizada com sucesso", new { totalCount = paged.TotalItems });

                return Ok(paged);
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Erro ao listar registros", ex, new { queryParams });
                return ErrorResponse("Erro interno ao listar registros", 500);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                await LogDebugAsync("Iniciando busca por ID", new { id });

                if (_authorizationService != null && !await _authorizationService.CanReadAsync())
                {
                    return await _authorizationService.GetUnauthorizedResponseAsync();
                }

                if (_validationService != null)
                {
                    var validationResult = await _validationService.ValidateIdAsync(id);
                    if (validationResult != null) return validationResult;
                }

                var dto = await _service.ObterPorId(id);
                if (dto == null) return NotFoundResponse();

                await LogInformationAsync("Registro encontrado com sucesso", new { id });
                return Ok(new ApiResponse<TDto>(dto));
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Erro ao obter registro por ID", ex, new { id });
                return ErrorResponse("Erro interno ao obter registro", 500);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Criar([FromBody] TCreateDto dto)
        {
            try
            {
                await LogDebugAsync("Iniciando criação de registro", new { dto });

                if (_authorizationService != null && !await _authorizationService.CanCreateAsync())
                {
                    return await _authorizationService.GetUnauthorizedResponseAsync();
                }

                if (_validationService != null)
                {
                    var validationResult = await _validationService.ValidateCreateAsync(dto);
                    if (validationResult != null) return validationResult;
                }

                var created = await _service.Criar(dto);
                var id = GetId(created);

                await LogInformationAsync("Registro criado com sucesso", new { id, dto });
                return CreatedAtAction(nameof(ObterPorId), new { id }, new ApiResponse<TDto>(created));
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Erro ao criar registro", ex, new { dto });
                return ErrorResponse("Erro interno ao criar registro", 500);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Atualizar(Guid id, [FromBody] TUpdateDto dto)
        {
            try
            {
                await LogDebugAsync("Iniciando atualização de registro", new { id, dto });

                if (_authorizationService != null && !await _authorizationService.CanUpdateAsync(id))
                {
                    return await _authorizationService.GetUnauthorizedResponseAsync();
                }

                if (_validationService != null)
                {
                    var validationResult = await _validationService.ValidateUpdateAsync(id, dto);
                    if (validationResult != null) return validationResult;
                }

                var updated = await _service.Atualizar(id, dto);
                if (updated == null) return NotFoundResponse();

                await LogInformationAsync("Registro atualizado com sucesso", new { id, dto });
                return Ok(new ApiResponse<TDto>(updated));
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Erro ao atualizar registro", ex, new { id, dto });
                return ErrorResponse("Erro interno ao atualizar registro", 500);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Remover(Guid id)
        {
            try
            {
                await LogDebugAsync("Iniciando remoção de registro", new { id });

                if (_authorizationService != null && !await _authorizationService.CanDeleteAsync(id))
                {
                    return await _authorizationService.GetUnauthorizedResponseAsync();
                }

                if (_validationService != null)
                {
                    var validationResult = await _validationService.ValidateIdAsync(id);
                    if (validationResult != null) return validationResult;
                }

                var sucesso = await _service.Remover(id);
                if (!sucesso) return NotFoundResponse();

                await LogInformationAsync("Registro removido com sucesso", new { id });
                return NoContent();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Erro ao remover registro", ex, new { id });
                return ErrorResponse("Erro interno ao remover registro", 500);
            }
        }

        // Helper para obter o Id do DTO criado
        protected virtual Guid GetId(TDto dto)
        {
            var prop = typeof(TDto).GetProperty("Id");
            return prop != null ? (Guid)prop.GetValue(dto) : Guid.Empty;
        }

        [NonAction]
        protected IActionResult ErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new ApiResponse<object>(default) { Success = false, Message = message });
        }

        [NonAction]
        protected IActionResult NotFoundResponse(string message = "Recurso não encontrado.")
        {
            return NotFound(new ApiResponse<object>(default) { Success = false, Message = message });
        }

        // Métodos de logging com fallback
        private async Task LogDebugAsync(string message, object? data = null)
        {
            if (_loggingService != null)
                await _loggingService.LogDebugAsync(message, data);
        }

        private async Task LogInformationAsync(string message, object? data = null)
        {
            if (_loggingService != null)
                await _loggingService.LogInformationAsync(message, data);
        }

        private async Task LogErrorAsync(string message, Exception? exception = null, object? data = null)
        {
            if (_loggingService != null)
                await _loggingService.LogErrorAsync(message, exception, data);
        }
    }
}
