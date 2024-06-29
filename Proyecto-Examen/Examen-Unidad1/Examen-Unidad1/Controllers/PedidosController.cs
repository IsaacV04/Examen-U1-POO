using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Examen_Unidad1.Controllers
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IOrdersService _pedidosService;

        public PedidosController(IOrdersService pedidosService)
        {
            _pedidosService = pedidosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var pedidos = await _pedidosService.GetPedidosListAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var pedido = await _pedidosService.GetPedidosByIdAsync(id);
            if (pedido == null)
            {
                return NotFound(new { Message = $"No se encontró el pedido con ID: {id}" });
            }
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PedidoCreateDto dto)
        {
            var success = await _pedidosService.CreateAsync(dto);
            if (!success)
            {
                return BadRequest(new { Message = "Error al crear el pedido." });
            }
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id, PedidoEditDto dto)
        {
            var success = await _pedidosService.EditAsync(dto, id);
            if (!success)
            {
                return NotFound(new { Message = $"No se encontró el pedido con ID: {id}" });
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var success = await _pedidosService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = $"No se encontró el pedido con ID: {id}" });
            }
            return Ok();
        }
    }
}