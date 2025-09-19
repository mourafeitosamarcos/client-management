using ClientManagement.Application.Dto;

namespace ClientManagement.Application.Interfaces.Services
{
    public interface ISaleService
    {
        Task<SaleDTO> GetById(int id);
        Task<List<SaleDTO>> GetAll();
        Task<SaleDTO> Add(SaleDTO sale);
        Task<SaleDTO> Update(SaleDTO sale);
        Task<bool> Delete(int id);
        Task<List<SaleDTO>> GetByClientId(int clientId);
        Task<List<SaleDTO>> GetByProductId(int productId);
    }
}
