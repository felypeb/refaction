using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RefactorMe.Model.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<TEntity> CreateAsync(TEntity obj);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

        Task RemoveAsync(TEntity obj);

        Task RemoveByIdAsync(TKey id);

        Task<int> SaveChangesAsync();

        Task<TEntity> UpdateAsync(TEntity obj);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}