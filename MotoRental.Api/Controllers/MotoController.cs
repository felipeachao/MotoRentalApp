using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Application.Commands;
using MotoRental.Application.Queries;
using System.Threading.Tasks;

namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="command">Dados da moto</param>
        /// <returns>O ID da moto cadastrada</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMotoCommand command)
        {
            var motoId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = motoId }, null);
        }

        /// <summary>
        /// Consulta uma moto específica pelo ID.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Detalhes da moto</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _mediator.Send(new GetMotoByIdQuery { MotoId = id });
            return moto == null ? NotFound() : Ok(moto);
        }

        /// <summary>
        /// Consulta as motos existentes e filtra pela placa.
        /// </summary>
        /// <param name="licensePlate">Placa da moto</param>
        /// <returns>Lista de motos filtradas</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string licensePlate)
        {
            var motos = await _mediator.Send(new GetMotosQuery { LicensePlate = licensePlate });
            return Ok(motos);
        }

        /// <summary>
        /// Atualiza a placa de uma moto específica.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="newLicensePlate">Nova placa</param>
        /// <returns>Status da atualização</returns>
        [HttpPut("{id}/license-plate")]
        public async Task<IActionResult> UpdateLicensePlate(Guid id, [FromBody] string newLicensePlate)
        {
            var command = new UpdateMotoLicensePlateCommand { MotoId = id, NewLicensePlate = newLicensePlate };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Remove uma moto pelo ID, desde que não tenha locações ativas.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Status da remoção</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteMotoCommand { MotoId = id });
            return NoContent();
        }
    }
}
