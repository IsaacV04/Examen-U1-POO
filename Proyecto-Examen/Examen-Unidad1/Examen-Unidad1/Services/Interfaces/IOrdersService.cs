using Examen_Unidad1.Dtos.Pedidos;

namespace Examen_Unidad1.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<List<PedidosDto>> GetPedidosListAsync();
        Task<PedidosDto> GetPedidosByIdAsync(Guid id);
        Task<bool> CreateAsync(PedidoCreateDto dto);
        Task<bool> EditAsync(PedidoEditDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}

