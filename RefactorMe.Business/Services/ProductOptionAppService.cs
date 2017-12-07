using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using RefactorMe.Application.ApiModels;
using RefactorMe.Application.Interfaces;
using RefactorMe.Model.Entities;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Model.Interfaces.Service;

namespace RefactorMe.Application.Services
{
    public class ProductOptionAppService : AppServiceBase, IProductOptionAppService
    {
        private readonly IProductOptionService _productOptionService;

        public ProductOptionAppService(IProductOptionService productOptionService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._productOptionService = productOptionService;
        }

        public async Task<ProductOptionsApiModel> ListByProductIdAsync(Guid id)
        {
            var productOptions = await this._productOptionService.ListByProductIdAsync(id);

            return new ProductOptionsApiModel
            {
                Items = productOptions.Adapt<IEnumerable<ProductOptionApiModel>>()
            };
        }

        public async Task<ProductOptionApiModel> GetByIdAsync(Guid productId, Guid id)
        {
            var productOption = await this._productOptionService.GetByIdAsync(productId, id);

            return productOption.Adapt<ProductOptionApiModel>();
        }

        public async Task<ProductOptionApiModel> CreateAsync(ProductOptionApiModel option)
        {
            // Transaction is being used here just as an example (let's consider that more than one operation could happen below)
            this.BeginTransaction();

            var newOption = await this._productOptionService.CreateAsync(option.Adapt<ProductOption>());

            this.Commit();

            return newOption.Adapt<ProductOptionApiModel>();
        }

        public async Task UpdateAsync(ProductOptionApiModel option)
        {
            await this._productOptionService.UpdateAsync(option.Adapt<ProductOption>());
        }

        public async Task RemoveAsync(ProductOptionApiModel option)
        {
            await this._productOptionService.RemoveAsync(option.Adapt<ProductOption>());
        }
    }
}
