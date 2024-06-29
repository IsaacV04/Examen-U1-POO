using BlogUNAH.API.Services.Interfaces;
using Examen_Unidad1.Database.Entities;
using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_Unidad1.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductsService _productosService;

        public ProductosController(IProductsService categoriesService)
        {
            this._productosService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productosService.GetProductsListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var producto = await _productosService.GetProductsByIdAsync(id);

            if (producto == null)
            {
                return NotFound(new { Message = $"No se encontro el producto: {id}" });
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductsCreateDto dto)
        {
            await _productosService.CreateAsync(dto);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(ProductsEditDto dto, Guid id)
        {
            var result = await _productosService.EditAsync(dto, id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _productosService.GetProductsByIdAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            await _productosService.DeleteAsync(id);

            return Ok();

        }

    }
}