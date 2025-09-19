using ClientManagement.Application.Interfaces.Repository;

namespace ClientManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }
        IProductRepository ProductRepository { get; }
        ISaleRepository SaleRepository { get; }
    }
}
