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
    public class ProductAppService : AppServiceBase, IProductAppService
    {
        private readonly IProductService _productService;

        public ProductAppService(IProductService productService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._productService = productService;
        }

        public async Task<ProductsApiModel> ListAsync()
        {
            var products = await this._productService.ListAsync();
            return new ProductsApiModel { Items = products.Adapt<IEnumerable<ProductApiModel>>() };
        }

        public async Task<ProductsApiModel> ListByNameAsync(string name)
        {
            var products = await this._productService.ListByNameAsync(name);
            return new ProductsApiModel { Items = products.Adapt<IEnumerable<ProductApiModel>>() };
        }

        public async Task<ProductApiModel> GetByIdAsync(Guid id)
        {
            var product = await this._productService.GetByIdAsync(id);
            return product.Adapt<ProductApiModel>();
        }

        public async Task<ProductApiModel> CreateAsync(ProductApiModel productApiModel)
        {
            // Transaction is being used here just as an example (let's consider that more than one operation could happen below)
            this.BeginTransaction();

            var newProduct = await this._productService.CreateAsync(productApiModel.Adapt<Product>());

            this.Commit();

            return newProduct.Adapt<ProductApiModel>();
        }

        public async Task RemoveAsync(ProductApiModel product)
        {
            await this._productService.RemoveAsync(product.Adapt<Product>());

            this.Commit();
        }

        public async Task UpdateAsync(ProductApiModel product)
        {
            await this._productService.UpdateAsync(product.Adapt<Product>());

            this.Commit();
        }
    }
}
