using System.Threading.Tasks;

namespace RefactorMe.Model.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        void Commit();

        void Rollback();

        void BeginTransaction();
    }
}