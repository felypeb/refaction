using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefactorMe.Model.Entities;

namespace RefactorMe.Model.Interfaces.Service
{
    public interface IProductOptionService
    {
        Task<IEnumerable<ProductOption>> ListByProductIdAsync(Guid productId);

        Task<ProductOption> GetByIdAsync(Guid productId, Guid id);

        Task<ProductOption> CreateAsync(ProductOption productOption);

        Task UpdateAsync(ProductOption productOption);

        Task RemoveAsync(ProductOption productOption);
    }
}
