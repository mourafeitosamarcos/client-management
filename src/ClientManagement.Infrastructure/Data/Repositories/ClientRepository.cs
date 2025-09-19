using ClientManagement.Application.Interfaces.Repository;
using ClientManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client, int>, IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
