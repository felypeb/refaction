using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Data.Context;

namespace RefactorMe.Infra.Data.Repository
{
    public abstract class Repository<TEntity, TKey> : IDisposable, IRepositoryBase<TEntity, TKey> where TEntity : class where TKey : struct
    {
        protected RefactorMeDataContext DBContext;

        protected DbSet<TEntity> DBSet;

        public Repository(RefactorMeDataContext dbContext){
            this.DBContext = dbContext;
            this.DBSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity obj){
            var entity = this.DBSet.Add(obj);

            await this.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate){
            return await this.DBSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(){
            return await this.DBSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id){
            return await this.DBSet.FindAsync(id);
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate){
            return await this.DBSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task RemoveAsync(TEntity obj){
            this.DBContext.Entry(obj).State = EntityState.Deleted;

            await this.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(TKey id){
            this.DBSet.Remove(await this.GetByIdAsync(id));

            await this.SaveChangesAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate){
            return await this.DBSet.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> UpdateAsync(TEntity obj){
            this.DBContext.Entry(obj).State = EntityState.Modified;

            await this.SaveChangesAsync();

            return obj;
        }

        public async Task<int> SaveChangesAsync(){
            return await this.DBContext.SaveChangesAsync();
        }

        public void Dispose(){
            this.DBContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}