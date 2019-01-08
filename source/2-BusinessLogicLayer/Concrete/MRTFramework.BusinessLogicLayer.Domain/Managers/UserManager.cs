using System;
using System.Collections.Generic;
using System.Linq;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.CacheAspect;
using MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.LogAspect;
using MRTFramework.CrossCuttingConcern.AspectOrientedProgramming.PostSharp.PerformanceAspect;
using MRTFramework.CrossCuttingConcern.Caching.Concrete.Microsoft;
using MRTFramework.CrossCuttingConcern.Caching.Concrete.Redis;
using MRTFramework.CrossCuttingConcern.Logging.Abstract;
using MRTFramework.CrossCuttingConcern.Logging.Concrete;
using MRTFramework.CrossCuttingConcern.Logging.Concrete.NLog;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.Model.BaseModels.Concrete;
using MRTFramework.Model.Enums;

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
        [LogAspect(typeof(NLogService), typeof(NLogGraylogLogger))]
        //[ExceptionAspect(typeof(NLogService), typeof(NLogDatabaseLogger))]
        //[CacheAspect(typeof(MicrosoftMemoryCacheManager),1)]
        [PerformanceAspect(typeof(NLogService),typeof(NLogGraylogLogger))]
        [PerformanceAspect(typeof(NLogService), typeof(NLogGraylogLogger),0)]
        //[CacheAspect(typeof(RedisCacheManager), 1)]
        //[CacheRemoveAspect(typeof(RedisCacheManager))]
        public List<User> GetAllUser(User user, string a, int b)
        {
            return _userDao.ToAllList().ToList();
        }
    }
}
