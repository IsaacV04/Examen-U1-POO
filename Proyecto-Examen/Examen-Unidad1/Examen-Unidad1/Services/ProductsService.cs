using BlogUNAH.API.Services.Interfaces;
using Examen_Unidad1.Database.Entities;
using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Newtonsoft.Json;

namespace Examen_Unidad1.Services
{
    public class ProductsService : IProductsService
    {
        public readonly string _JSON_FILE;

        public ProductsService()
        {
            _JSON_FILE = "SeedData/Productos.json";
        }

        public async Task<List<ProductsDto>> GetProductsListAsync()
        {
            return await ReadProductsFromFileAsync();
        }
        public async Task<ProductsDto> GetProductsByIdAsync(Guid id)
        {
            var productos = await ReadProductsFromFileAsync();
            ProductsDto producto = productos.FirstOrDefault(c => c.Id == id);
            return producto;
        }
        public async Task<bool> CreateAsync(ProductsCreateDto dto)
        {
            var productosDtos = await ReadProductsFromFileAsync();

            var productoDto = new ProductsDto
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
            };

            productosDtos.Add(productoDto);

            var productos = productosDtos.Select(x => new Producto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();

            await WriteProductsToFileAsync(productos);

            return true;
        }
        public async Task<bool> EditAsync(ProductsEditDto dto, Guid id)
        {
            var productosDto = await ReadProductsFromFileAsync();

            var existingProducto = productosDto
                .FirstOrDefault(producto => producto.Id == id);
            if (existingProducto is null)
            {
                return false;
            }

            for (int i = 0; i < productosDto.Count; i++)
            {
                if (productosDto[i].Id == id)
                {
                    productosDto[i].Name = dto.Name;
                    productosDto[i].Price = dto.Price;
                }
            }

            var productos = productosDto.Select(x => new Producto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();

            await WriteProductsToFileAsync(productos);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var productosDto = await ReadProductsFromFileAsync();
            var productoToDelete = productosDto.FirstOrDefault(x => x.Id == id);

            if (productoToDelete is null)
            {
                return false;
            }

            productosDto.Remove(productoToDelete);

            var productos = productosDto.Select(x => new Producto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();

            await WriteProductsToFileAsync(productos);

            return true;
        }

        private async Task<List<ProductsDto>> ReadProductsFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<ProductsDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);

            var productos = JsonConvert.DeserializeObject<List<Producto>>(json);

            var dtos = productos.Select(x => new ProductsDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();

            return dtos;
        }

        private async Task WriteProductsToFileAsync(List<Producto> productos)
        {
            var json = JsonConvert.SerializeObject(productos, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }

        }

    }
}
