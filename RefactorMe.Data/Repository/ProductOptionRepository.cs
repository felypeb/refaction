using System;
using RefactorMe.Model.Entities;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Data.Context;

namespace RefactorMe.Infra.Data.Repository
{
    public class ProductOptionRepository : Repository<ProductOption, Guid>, IProductOptionRepository
    {
        public ProductOptionRepository(RefactorMeDataContext dbContext) : base(dbContext) { }
    }
}
