using System.Collections.Generic;
using System.Linq;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.BusinessLogicLayer.Domain.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDao _userDao;

        public UserManager(IUserDao userDao)
        {
            _userDao = userDao;
        }


        //[TransactionScopeAspect(TransactionScopeOption.Required, IsolationLevel.ReadCommitted, 60)]

        //[FluentValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
            _userDao.Add(user);
            _userDao.Commit();
        }

        //[LogAspect(typeof(NLogService),typeof(NLogFileLogger))]
        //[LogAspect(typeof(NLogService), typeof(NLogDatabaseLogger))]
        //[ExceptionAspect(typeof(NLogService), typeof(NLogDatabaseLogger))]
        //[CacheAspect(typeof(MicrosoftMemoryCacheManager),1)]
        //[PerformanceAspect(typeof(NLogService),typeof(NLogDatabaseLogger))]
        //[CacheAspect(typeof(RedisCacheManager), 1)]
        //[CacheRemoveAspect(typeof(RedisCacheManager))]
        public List<User> GetAllUser(User user, string a, int b)
        {
            return _userDao.ToAllList().ToList();
        }
    }
}
