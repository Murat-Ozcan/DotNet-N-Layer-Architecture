using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.DataAccessLayer.DAOInterfaces.UnitOfWork;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Context;

namespace MRTFramework.DataAccessLayer.EntityFrameworkCore.UnitOfWork
{
    public class EfcDatabaseUnitOfWork : IDatabaseUnitOfWork
    {
        public EfcDatabaseUnitOfWork(IUserDao userDal, NorthwindContext context)
        {
            UserDal = userDal;
            Context = context;
        }

        public IUserDao UserDal { get; }
        private NorthwindContext Context { get; }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
