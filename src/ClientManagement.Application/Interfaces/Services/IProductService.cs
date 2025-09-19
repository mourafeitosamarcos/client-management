using ClientManagement.Application.Dto;

namespace ClientManagement.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDTO?> GetById(int id);
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> Add(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(int id);
        Task<List<ProductDTO>> GetByRangePrice(decimal minPrice, decimal maxPrice);
    }
}
