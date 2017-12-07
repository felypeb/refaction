using System;
using System.Threading.Tasks;
using RefactorMe.Application.ApiModels;

namespace RefactorMe.Application.Interfaces
{
    public interface IProductOptionAppService
    {
        Task<ProductOptionsApiModel> ListByProductIdAsync(Guid productId);

        Task<ProductOptionApiModel> GetByIdAsync(Guid productId, Guid id);

        Task<ProductOptionApiModel> CreateAsync(ProductOptionApiModel option);

        Task UpdateAsync(ProductOptionApiModel option);

        Task RemoveAsync(ProductOptionApiModel option);
    }
}