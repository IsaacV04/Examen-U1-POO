using Examen_Unidad1.Database.Entities;
using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_Unidad1.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IOrdersService _pedidosService;

        public PedidosController(IOrdersService pedidosService)
        {
            this._pedidosService = pedidosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _pedidosService.GetPedidosListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var category = await _pedidosService.GetPedidosByIdAsync(id);

            if (category == null)
            {
                return NotFound(new { Message = $"No se encontro el pedido: {id}" });
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PedidoCreateDto dto)
        {
            await _pedidosService.CreateAsync(dto);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(PedidoEditDto dto, Guid id)
        {
            var result = await _pedidosService.EditAsync(dto, id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _pedidosService.GetPedidosByIdAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            await _pedidosService.DeleteAsync(id);

            return Ok();

        }

    }
}