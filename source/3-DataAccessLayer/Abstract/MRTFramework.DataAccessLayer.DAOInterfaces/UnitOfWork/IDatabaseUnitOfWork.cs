using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;

namespace MRTFramework.DataAccessLayer.DAOInterfaces.UnitOfWork
{
    public interface IDatabaseUnitOfWork
    {
        IUserDao UserDal { get; }
        void SaveChanges();
    }
}
