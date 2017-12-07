using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RefactorMe.Model.Entities;

namespace RefactorMe.Model.Interfaces.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();

        Task<IEnumerable<Product>> ListByNameAsync(string name);

        Task<Product> GetByIdAsync(Guid id);

        Task<Product> CreateAsync(Product product);

        Task RemoveAsync(Product product);

        Task UpdateAsync(Product product);
    }
}
