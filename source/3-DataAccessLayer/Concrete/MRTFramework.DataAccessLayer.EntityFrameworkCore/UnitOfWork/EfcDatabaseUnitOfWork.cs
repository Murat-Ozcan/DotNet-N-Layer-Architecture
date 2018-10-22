using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.DataAccessLayer.DAOInterfaces.UnitOfWork;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Context;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Repositories;

namespace MRTFramework.DataAccessLayer.EntityFrameworkCore.UnitOfWork
{
    public class EfcDatabaseUnitOfWork : IDatabaseUnitOfWork
    {
        private IUserDao _userDal;

        public EfcDatabaseUnitOfWork(NorthwindContext context)
        {
            Context = context;
        }

        public IUserDao UserDal => _userDal ?? (_userDal = new EfcUserDao(Context));
        private NorthwindContext Context { get; }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
