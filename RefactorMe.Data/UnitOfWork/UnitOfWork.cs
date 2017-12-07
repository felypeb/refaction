using System;
using System.Data.Entity;
using System.Threading.Tasks;
using RefactorMe.Model.Interfaces.Repository;
using RefactorMe.Infra.Data.Context;

namespace RefactorMe.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RefactorMeDataContext _dataContext;
        private DbContextTransaction _dbContextTransaction;

        public UnitOfWork(RefactorMeDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public void BeginTransaction()
        {
            this._dbContextTransaction = this._dataContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            this._dataContext?.Dispose();
            this._dbContextTransaction?.Dispose();
        }

        public void Commit()
        {
            this._dbContextTransaction?.Commit();
        }

        public void Rollback()
        {
            this._dbContextTransaction?.Rollback();
        }
    }
}