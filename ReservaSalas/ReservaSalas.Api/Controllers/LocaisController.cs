
using Microsoft.AspNetCore.Mvc;
using ReservaSalas.Aplicacao.DTOs.Local;
using ReservaSalas.Aplicacao.DTOs.Sala;
using ReservaSalas.Aplicacao.Servico;

namespace ReservaSalas.Api.Controllers
{

    [ApiController]
    [Route("api/locais")]
    public class LocaisController : ControllerBase
    {
        private readonly LocaisAppService _svc;

        public LocaisController(LocaisAppService svc) => _svc = svc;

        /// <summary>Lista todos os locais.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<LocalDto>>> GetLocais(CancellationToken ct) =>
            Ok(await _svc.ListarLocaisAsync(ct));

        /// <summary>Lista as salas de um local específico.</summary>
        [HttpGet("{localId:guid}/salas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<SalaDto>>> GetSalasPorLocal(Guid localId, CancellationToken ct) =>
            Ok(await _svc.ListarSalasPorLocalAsync(localId, ct));
    }

}
