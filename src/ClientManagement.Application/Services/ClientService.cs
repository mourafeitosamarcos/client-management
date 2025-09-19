using ClientManagement.Application.Dto;
using ClientManagement.Application.Interfaces;
using ClientManagement.Application.Interfaces.Services;
using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        public ClientService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtService jwtService) : base(unitOfWork)
        {
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ClientDTO> Add(ClientDTO client)
        {
            var entity = new Client
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                Password = _passwordHasher.Hash(client.Password),
                DataCadastro = DateTime.UtcNow,
                Ativo = true
            };

            await _unitOfWork.ClientRepository.SaveAsync(entity).ContinueWith(task =>
            {
                var client = task.Result;
                if (client == null) return null;
                return new ClientDTO
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    Address = client.Address,
                    DataCadastro = client.DataCadastro,
                    DataEdicao = client.DataEdicao,
                    Ativo = client.Ativo
                };
            });

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client == null)
                return false;

            await _unitOfWork.ClientRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ClientDTO>> GetAll()
        {
            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            return clients.Select(c => new ClientDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                DataCadastro = c.DataCadastro,
                DataEdicao = c.DataEdicao,
                Ativo = c.Ativo
            }).ToList();
        }

        public async Task<ClientDTO?> GetById(int id)
        {
            return await _unitOfWork.ClientRepository.GetByIdAsync(id).ContinueWith(task =>
            {
                var client = task.Result;
                if (client == null) return null;
                return new ClientDTO
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    Address = client.Address,
                    DataCadastro = client.DataCadastro,
                    DataEdicao = client.DataEdicao,
                    Ativo = client.Ativo
                };
            });
        }

        public async Task<ClientDTO> Update(ClientDTO client)
        {
            var entity = await _unitOfWork.ClientRepository.GetByIdAsync(client.Id);
            if (entity == null)
                return null;

            entity.Name = client.Name;
            entity.Email = client.Email;
            entity.Phone = client.Phone;
            entity.Address = client.Address;
            entity.Password = _passwordHasher.Hash(client.Password); 
            entity.DataEdicao = DateTime.UtcNow;
            entity.Ativo = client.Ativo;

            await _unitOfWork.ClientRepository.UpdateAsync(entity);

            client.DataEdicao = entity.DataEdicao;
            return client;
        }

        public async Task<string> CheckLogin(LoginDTO login)
        {
            Client? client = await _unitOfWork.ClientRepository.GetByEmailAsync(login.Email);

            if (client == null || !_passwordHasher.Verify(login.Password, client.Password))
                return null;

            return await _jwtService.GenerateToken(client, new List<string>(), null);
        }
    }
}
