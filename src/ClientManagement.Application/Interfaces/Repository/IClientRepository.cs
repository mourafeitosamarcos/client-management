using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Interfaces.Repository
{
    public interface IClientRepository : IBaseRepository<Client, int>
    {
        Task<Client?> GetByEmailAsync(string email);
    }
}
