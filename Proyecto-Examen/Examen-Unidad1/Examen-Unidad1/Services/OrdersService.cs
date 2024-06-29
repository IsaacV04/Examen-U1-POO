using Examen_Unidad1.Database.Entities;
using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Newtonsoft.Json;

namespace Examen_Unidad1.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly string _JSON_FILE;
        private readonly string _PRODUCTS_JSON_FILE;

        public OrdersService()
        {
            _JSON_FILE = "SeedData/Pedidos.json";
            _PRODUCTS_JSON_FILE = "SeedData/Productos.json";
        }

        public async Task<List<PedidosDto>> GetPedidosListAsync()
        {
            return await ReadPedidosFromFileAsync();
        }

        public async Task<PedidosDto> GetPedidosByIdAsync(Guid id)
        {
            var pedidos = await ReadPedidosFromFileAsync();
            return pedidos.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(PedidoCreateDto dto)
        {
            try
            {
                var pedidosDtos = await ReadPedidosFromFileAsync();
                var pedidoDto = new PedidosDto
                {
                    Id = Guid.NewGuid(),
                    ClienteId = dto.ClienteId,
                    Fecha = dto.Fecha,
                    ListaDeProductos = dto.ListaDeProductos,
                    Total = await CalcularTotal(dto.ListaDeProductos)
                };

                pedidosDtos.Add(pedidoDto);

                var pedidos = pedidosDtos.Select(x => new Pedido
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Fecha = x.Fecha,
                    ListaDeProductos = x.ListaDeProductos,
                    Total = x.Total,
                }).ToList();

                await WritePedidosToFileAsync(pedidos);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditAsync(PedidoEditDto dto, Guid id)
        {
            try
            {
                var pedidosDto = await ReadPedidosFromFileAsync();
                var existingPedido = pedidosDto.FirstOrDefault(pedido => pedido.Id == id);
                if (existingPedido == null) return false;

                existingPedido.ListaDeProductos = dto.ListaDeProductos;
                existingPedido.Total = await CalcularTotal(dto.ListaDeProductos);

                var pedidos = pedidosDto.Select(x => new Pedido
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Fecha = x.Fecha,
                    ListaDeProductos = x.ListaDeProductos,
                    Total = x.Total,
                }).ToList();

                await WritePedidosToFileAsync(pedidos);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var pedidosDto = await ReadPedidosFromFileAsync();
                var pedidoToDelete = pedidosDto.FirstOrDefault(x => x.Id == id);
                if (pedidoToDelete == null) return false;

                pedidosDto.Remove(pedidoToDelete);

                var pedidos = pedidosDto.Select(x => new Pedido
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Fecha = x.Fecha,
                    ListaDeProductos = x.ListaDeProductos,
                    Total = x.Total,
                }).ToList();

                await WritePedidosToFileAsync(pedidos);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteAsync: {ex.Message}");
                return false;
            }
        }

        private async Task<List<PedidosDto>> ReadPedidosFromFileAsync()
        {
            try
            {
                if (!File.Exists(_JSON_FILE))
                {
                    return new List<PedidosDto>();
                }

                var json = await File.ReadAllTextAsync(_JSON_FILE);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json);
                return pedidos?.Select(x => new PedidosDto
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Fecha = x.Fecha,
                    ListaDeProductos = x.ListaDeProductos,
                    Total = x.Total
                }).ToList() ?? new List<PedidosDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReadPedidosFromFileAsync: {ex.Message}");
                return new List<PedidosDto>();
            }
        }

        private async Task WritePedidosToFileAsync(List<Pedido> pedidos)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en WritePedidosToFileAsync: {ex.Message}");
            }
        }

        private async Task<decimal> CalcularTotal(string listaDeProductos)
        {
            try
            {
                var productos = await ReadProductsFromFileAsync();
                var productosIds = listaDeProductos.Split(',').Select(p => Guid.Parse(p.Trim())).ToList();
                return productos.Where(p => productosIds.Contains(p.Id)).Sum(p => p.Price);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CalcularTotal: {ex.Message}");
                return 0;
            }
        }

        private async Task<List<Producto>> ReadProductsFromFileAsync()
        {
            try
            {
                if (!File.Exists(_PRODUCTS_JSON_FILE)) return new List<Producto>();

                var json = await File.ReadAllTextAsync(_PRODUCTS_JSON_FILE);
                return JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReadProductsFromFileAsync: {ex.Message}");
                return new List<Producto>();
            }
        }
    }
}