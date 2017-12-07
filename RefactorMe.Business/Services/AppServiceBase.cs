using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorMe.Model.Interfaces.Repository;

namespace RefactorMe.Application.Services
{
    public class AppServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceBase(IUnitOfWork unitOfWork){
            this._unitOfWork = unitOfWork;
        }

        public void Commit(){
            this._unitOfWork.Commit();
        }

        public void RollBack()
        {
            this._unitOfWork.Rollback();
        }

        public void BeginTransaction(){
            this._unitOfWork.BeginTransaction();
        }
    }
}
