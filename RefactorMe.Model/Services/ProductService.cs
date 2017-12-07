using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefactorMe.Model.Entities;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Model.Interfaces.Service;

namespace RefactorMe.Model.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await this._productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> ListByNameAsync(string name)
        {
            return await this._productRepository.ListAsync(p => p.Name.Contains(name));
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await this._productRepository.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            return await this._productRepository.CreateAsync(product);
        }

        public async Task RemoveAsync(Product product)
        {
            await this._productRepository.RemoveAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await this._productRepository.UpdateAsync(product);
        }
    }
}