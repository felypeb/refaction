using System;
using System.Threading.Tasks;
using RefactorMe.Application.ApiModels;

namespace RefactorMe.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductsApiModel> ListAsync();

        Task<ProductsApiModel> ListByNameAsync(string name);

        Task<ProductApiModel> GetByIdAsync(Guid id);

        Task<ProductApiModel> CreateAsync(ProductApiModel productApiModel);

        Task RemoveAsync(ProductApiModel productApiModel);

        Task UpdateAsync(ProductApiModel product);
    }
}