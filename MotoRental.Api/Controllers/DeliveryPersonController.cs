using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Application.Commands;
using System.Threading.Tasks;

namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryPersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra um novo entregador.
        /// </summary>
        /// <param name="command">Dados do entregador</param>
        /// <returns>O ID do entregador cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryPersonCommand command)
        {
            var deliveryPersonId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = deliveryPersonId }, null);
        }

        /// <summary>
        /// Consulta um entregador específico pelo ID.
        /// </summary>
        /// <param name="id">ID do entregador</param>
        /// <returns>Detalhes do entregador</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Upload da imagem da CNH do entregador.
        /// </summary>
        /// <param name="id">ID do entregador</param>
        /// <param name="image">Imagem da CNH em formato PNG ou BMP</param>
        /// <returns>Status do upload</returns>
        [HttpPost("{id}/upload-cnh")]
        public async Task<IActionResult> UploadCnh(Guid id, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");
            var command = new UploadCnhCommand { DeliveryPersonId = id, Image = image };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
