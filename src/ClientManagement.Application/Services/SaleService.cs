using ClientManagement.Application.Dto;
using ClientManagement.Application.Interfaces;
using ClientManagement.Application.Interfaces.Services;
using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Services
{
    public class SaleService : BaseService, ISaleService
    {
        public SaleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<SaleDTO> Add(SaleDTO sale)
        {
            var saleEntity = new Sale
            {
                ClientId = sale.ClientId,
                Date = sale.Date,
                Total = sale.Total,
                Items = sale.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            var result = await _unitOfWork.SaleRepository.SaveAsync(saleEntity);
            sale.Id = result.Id;
            return sale;
        }

        public async Task<bool> Delete(int id)
        {
            var sale = await _unitOfWork.SaleRepository.GetByIdAsync(id);
            if (sale == null)
                return false;

            await _unitOfWork.SaleRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<SaleDTO>> GetAll()
        {
            var sales = await _unitOfWork.SaleRepository.GetAllAsync();
            return sales.Select(s => new SaleDTO
            {
                Id = s.Id,
                ClientId = s.ClientId,
                Date = s.Date,
                Total = s.Total,
                Items = s.Items.Select(i => new SaleItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }

        public async Task<List<SaleDTO>> GetByClientId(int clientId)
        {
            var sales = await _unitOfWork.SaleRepository.GetByClientIdAsync(clientId);
            return sales.Select(s => new SaleDTO
            {
                Id = s.Id,
                ClientId = s.ClientId,
                Date = s.Date,
                Total = s.Total,
                Items = s.Items.Select(i => new SaleItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }

        public async Task<SaleDTO> GetById(int id)
        {
            var s = await _unitOfWork.SaleRepository.GetByIdAsync(id);
            if (s == null) return null;
            return new SaleDTO
            {
                Id = s.Id,
                ClientId = s.ClientId,
                Date = s.Date,
                Total = s.Total,
                Items = s.Items.Select(i => new SaleItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }

        public async Task<List<SaleDTO>> GetByProductId(int productId)
        {
            var sales = await _unitOfWork.SaleRepository.GetByProductIdAsync(productId);
            return sales.Select(s => new SaleDTO
            {
                Id = s.Id,
                ClientId = s.ClientId,
                Date = s.Date,
                Total = s.Total,
                Items = s.Items.Select(i => new SaleItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }

        public async Task<SaleDTO> Update(SaleDTO sale)
        {
            var saleEntity = await _unitOfWork.SaleRepository.GetByIdAsync(sale.Id);
            if (saleEntity == null) return null;

            saleEntity.ClientId = sale.ClientId;
            saleEntity.Date = sale.Date;
            saleEntity.Total = sale.Total;

            await _unitOfWork.SaleRepository.UpdateAsync(saleEntity);
            return sale;
        }
    }
}
