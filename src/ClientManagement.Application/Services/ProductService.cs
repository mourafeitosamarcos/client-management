using ClientManagement.Application.Dto;
using ClientManagement.Application.Interfaces;
using ClientManagement.Application.Interfaces.Services;
using ClientManagement.Domain.Entities;

namespace ClientManagement.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ProductDTO> Add(ProductDTO productDTO)
        {
            Product product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                DataCadastro = DateTime.Now
            };

            await _unitOfWork.ProductRepository.SaveAsync(product);

            productDTO.Id = product.Id;
            productDTO.DataCadastro = product.DataCadastro;

            return productDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.ProductRepository.DeleteAsync(id);
            return result != null;

        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var ret = await _unitOfWork.ProductRepository.GetAllAsync();

            return ret.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                DataCadastro = product.DataCadastro
            }).ToList();
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id).ContinueWith(task =>
            {
                var product = task.Result;
                if (product == null) return null;
                return new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    DataCadastro = product.DataCadastro
                };
            });
        }

        public async Task<List<ProductDTO>> GetByRangePrice(decimal minPrice, decimal maxPrice)
        {
            var ret = await _unitOfWork.ProductRepository.GetByRangePrice(minPrice, maxPrice);

            return ret.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                DataCadastro = product.DataCadastro
            }).ToList();
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            Product product = await _unitOfWork.ProductRepository.GetByIdAsync(productDTO.Id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with Id {productDTO.Id} not found.");
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.DataEdicao = DateTime.Now;

            await _unitOfWork.ProductRepository.UpdateAsync(product);

            productDTO.DataEdicao = product.DataEdicao;
            return productDTO;
        }
    }
}
