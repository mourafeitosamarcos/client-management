using ClientManagement.Application.Interfaces.Repository;
using ClientManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Data.Repositories
{
    public class SaleRepository : BaseRepository<Sale, int>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetByProductIdAsync(int produtoId)
        {
            return await _context.Sale
                               .Include(s => s.Items)
                               .Where(s => s.Items.Any(i => i.ProductId == produtoId))
                               .ToListAsync();
        }
        public async Task<List<Sale>> GetByClientIdAsync(int clientId)
        {
            return await _context.Sale
                .Include(s => s.Items)
                .Where(s => s.ClientId == clientId)
                .ToListAsync();
        }
    }
}