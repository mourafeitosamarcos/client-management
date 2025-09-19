using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Interfaces.Repository
{
    public interface ISaleRepository : IBaseRepository<Sale, int>
    {
        Task<List<Sale>> GetByProductIdAsync(int produtoId);
        Task<List<Sale>> GetByClientIdAsync(int clientId);
    }
}
