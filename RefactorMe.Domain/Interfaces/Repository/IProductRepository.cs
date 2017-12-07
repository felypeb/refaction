using System;
using RefactorMe.Model.Entities;

namespace RefactorMe.Model.Interfaces.Repository
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
    }
}
