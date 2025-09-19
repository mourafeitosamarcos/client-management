using ClientManagement.Application.Interfaces.Repository;
using ClientManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetByRangePrice(decimal minPrice, decimal maxPrice)
        {
            return await _context.Product
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }
    }
}
