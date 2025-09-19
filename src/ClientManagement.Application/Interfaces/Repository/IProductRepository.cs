using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Interfaces.Repository
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        Task<List<Product>> GetByRangePrice(decimal minPrice, decimal maxPrice);
    }
}
