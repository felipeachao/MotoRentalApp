using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Application.Commands;
using System.Threading.Tasks;

namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova locação de moto.
        /// </summary>
        /// <param name="command">Dados da locação</param>
        /// <returns>O ID da locação criada</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRentalCommand command)
        {
            var rentalId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = rentalId }, null);
        }

        /// <summary>
        /// Consulta uma locação específica pelo ID.
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Detalhes da locação</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Atualiza a locação com a data de devolução e calcula multas se aplicável.
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <param name="returnDate">Data real de devolução</param>
        /// <returns>O valor total da locação incluindo multas</returns>
        [HttpPut("{id}/return")]
        public async Task<IActionResult> UpdateReturn(Guid id, [FromBody] DateTime returnDate)
        {
            var command = new UpdateRentalCommand { RentalId = id, ActualReturnDate = returnDate };
            var totalCost = await _mediator.Send(command);
            return Ok(totalCost);
        }
    }
}
