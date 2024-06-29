using Examen_Unidad1.Dtos.Pedidos;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductsDto>> GetProductsListAsync();
        Task<ProductsDto> GetProductsByIdAsync(Guid id);
        Task<bool> CreateAsync(ProductsCreateDto dto);
        Task<bool> EditAsync(ProductsEditDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}