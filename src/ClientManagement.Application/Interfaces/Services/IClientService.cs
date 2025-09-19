using ClientManagement.Application.Dto;

namespace ClientManagement.Application.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientDTO?> GetById(int id);
        Task<List<ClientDTO?>> GetAll();
        Task<ClientDTO> Add(ClientDTO client);
        Task<ClientDTO> Update(ClientDTO client);
        Task<bool> Delete(int id);
        Task<string> CheckLogin(LoginDTO login);
    }
}
