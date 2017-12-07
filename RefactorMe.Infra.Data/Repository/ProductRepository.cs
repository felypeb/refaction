using System;
using RefactorMe.Model.Entities;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Data.Context;

namespace RefactorMe.Infra.Data.Repository
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(RefactorMeDataContext dbContext) : base(dbContext) { }
    }
}
