using ClientManagement.Application.Interfaces;
using ClientManagement.Application.Interfaces.Repository;
using ClientManagement.Infrastructure.Data.Repositories;

namespace ClientManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IClientRepository _clientRepository;
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_context);
                }

                return _clientRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        public ISaleRepository SaleRepository
        {
            get
            {
                if (_saleRepository == null)
                {
                    _saleRepository = new SaleRepository(_context);
                }

                return _saleRepository;
            }
        }

    }
}
