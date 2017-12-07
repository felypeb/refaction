using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefactorMe.Model.Entities;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Model.Interfaces.Service;

namespace RefactorMe.Model.Services
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IProductOptionRepository productOptionRepository)
        {
            this._productOptionRepository = productOptionRepository;
        }

        public async Task<IEnumerable<ProductOption>> ListByProductIdAsync(Guid productId)
        {
            return await this._productOptionRepository.ListAsync(p => p.ProductId == productId);
        }

        public async Task<ProductOption> GetByIdAsync(Guid productId, Guid id)
        {
            return await this._productOptionRepository
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.Id == id);
        }

        public async Task<ProductOption> CreateAsync(ProductOption productOption)
        {
            return await this._productOptionRepository.CreateAsync(productOption);
        }

        public async Task UpdateAsync(ProductOption productOption)
        {
            if (productOption.ProductId == Guid.Empty)
            {
                var current = await this._productOptionRepository.GetByIdAsync(productOption.Id);
                if (current == null)
                    return;

                productOption.ProductId = current.ProductId;
            }

            await this._productOptionRepository.UpdateAsync(productOption);
        }

        public async Task RemoveAsync(ProductOption productOption)
        {
            await this._productOptionRepository.RemoveAsync(productOption);
        }
    }
}