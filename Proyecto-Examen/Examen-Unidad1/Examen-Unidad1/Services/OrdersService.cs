using Examen_Unidad1.Database.Entities;
using Examen_Unidad1.Dtos.Pedidos;
using Examen_Unidad1.Services.Interfaces;
using Newtonsoft.Json;

namespace Examen_Unidad1.Services
{
    public class OrdersService : IOrdersService
    {
        public readonly string _JSON_FILE;

        public OrdersService()
        {
            _JSON_FILE = "SeedData/Pedidos.json";
        }

        public async Task<List<PedidosDto>> GetPedidosListAsync()
        {
            return await ReadPedidosFromFileAsync();
        }
        public async Task<PedidosDto> GetPedidosByIdAsync(Guid id)
        {
            var pedidos = await ReadPedidosFromFileAsync();
            PedidosDto pedido = pedidos.FirstOrDefault(c => c.Id == id);
            return pedido;
        }
        public async Task<bool> CreateAsync(PedidoCreateDto dto)
        {
            var pedidosDtos = await ReadPedidosFromFileAsync();

            var pedidoDto = new PedidosDto
            {
                Id = Guid.NewGuid(),
                ListaDeProductos = dto.ListaDeProductos,
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
        public async Task<bool> EditAsync(PedidoEditDto dto, Guid id)
        {
            var pedidosDto = await ReadPedidosFromFileAsync();

            var existingPedido = pedidosDto
                .FirstOrDefault(pedido => pedido.Id == id);
            if (existingPedido is null)
            {
                return false;
            }

            for (int i = 0; i < pedidosDto.Count; i++)
            {
                if (pedidosDto[i].Id == id)
                {
                    pedidosDto[i].ListaDeProductos = dto.ListaDeProductos;
                }
            }

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
        public async Task<bool> DeleteAsync(Guid id)
        {
            var pedidosDto = await ReadPedidosFromFileAsync();
            var pedidoToDelete = pedidosDto.FirstOrDefault(x => x.Id == id);

            if (pedidoToDelete is null)
            {
                return false;
            }

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

        private async Task<List<PedidosDto>> ReadPedidosFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<PedidosDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);

            var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json);

            var dtos = pedidos.Select(x => new PedidosDto
            {
                Id = x.Id,
                ClienteId = x.ClienteId,
                Fecha = x.Fecha,
                ListaDeProductos = x.ListaDeProductos,
                Total = x.Total,
            }).ToList();

            return dtos;
        }

        private async Task WritePedidosToFileAsync(List<Pedido> pedidos)
        {
            var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }

        }

    }
}
