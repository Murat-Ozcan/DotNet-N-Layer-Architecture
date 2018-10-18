using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MRTFramework.BusinessLogicLayer.Domain.Managers;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.CrossCuttingConcern.Security;
using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.DataAccessLayer.DAOInterfaces.UnitOfWork;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Repositories;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.UnitOfWork;

namespace MRTFramework.CrossCuttingConcern.DependencyInjection
{
    public class DependencyInjector
    {
        private static IServiceProvider ServiceProvider { get; set; }

        private static IServiceCollection Services { get; set; }

        public static void AddDbContext<T>(string connectionString) where T : DbContext
        {
            Services.AddDbContextPool<T>(
                x => x.UseSqlServer
                    (connectionString, q => q.MigrationsAssembly("MRTFramework.SharedUI.Api"))
            );

            var context = GetService<T>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        public static T GetService<T>()
        {
            Services = Services ?? RegisterServices();
            ServiceProvider = ServiceProvider ?? Services.BuildServiceProvider();
            return ServiceProvider.GetService<T>();
        }

        public static IServiceCollection RegisterServices()
        {
            return RegisterServices(new ServiceCollection());
        }

        public static IServiceCollection RegisterServices(IServiceCollection service)
        {
            Services = service;

            // Solution - Data Access - Concrete / MRTFramework.DataAccess.EntityFrameworkCore

            Services.AddScoped<IDatabaseUnitOfWork, EfcDatabaseUnitOfWork>();
            Services.AddScoped<IUserDao, EfcUserDao>();

            // Solution - Business - Concrete / MRTFramework.Business.Domain

            Services.AddScoped<IUserService, UserManager>();
            Services.AddScoped<IAuthentication, AuthenticationManager>();

            // Solution.CrossCutting
            Services.AddScoped<IJsonWebToken, JsonWebToken>();
            return Services;
        }
    }
}
