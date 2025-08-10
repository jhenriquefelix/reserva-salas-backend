
using FluentValidation;
using ReservaSalas.Aplicacao.DTOs.Reserva;
using ReservaSalas.Aplicacao.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReservaSalas.Api.Controllers
{

    [ApiController]
    [Route("api/reservas")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaAppService _svc;
        private readonly IValidator<CriarReservaDto> _createVal;
        private readonly IValidator<AtualizarReservaDto> _updateVal;

        public ReservasController(
            ReservaAppService svc,
            IValidator<CriarReservaDto> createVal,
            IValidator<AtualizarReservaDto> updateVal)
        {
            _svc = svc;
            _createVal = createVal;
            _updateVal = updateVal;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReservaDto>>> Listar(
            [FromQuery] Guid? salaId,
            [FromQuery] DateTime? de,
            [FromQuery] DateTime? ate,
            CancellationToken ct)
        {
            var result = await _svc.ListarAsync(salaId, de, ate, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReservaDto>> Obter(Guid id, CancellationToken ct)
        {
            var r = await _svc.ObterAsync(id, ct);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Criar([FromBody] CriarReservaDto dto, CancellationToken ct)
        {
            // validação explícita (além da do service) para retornar 400 com detalhes
            var vr = await _createVal.ValidateAsync(dto, ct);
            if (!vr.IsValid) return ValidationProblem(new ValidationProblemDetails(vr.ToDictionary()));


            try
            {
                var id = await _svc.CriarAsync(dto, ct);
                return CreatedAtAction(nameof(Obter), new { id }, id);
            }
            catch (InvalidOperationException e) // conflito de horário
            {
                return Conflict(new { error = e.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarReservaDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest(new { error = "Id do path difere do body." });

            var vr = await _updateVal.ValidateAsync(dto, ct);
            if (!vr.IsValid) return ValidationProblem(new ValidationProblemDetails(vr.ToDictionary()));

            try
            {
                await _svc.AtualizarAsync(dto, ct);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { error = "A reserva foi alterada por outro usuário. Recarregue e tente novamente." });
            }
            catch (InvalidOperationException e) // conflito de horário
            {
                return Conflict(new { error = e.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id, CancellationToken ct)
        {
            await _svc.RemoverAsync(id, ct);
            return NoContent();
        }
    }

}
