using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRTFramework.CrossCuttingConcern.DependencyInjection;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Context;

namespace MRTFramework.SharedUI.Api
{
    public static class Extensions
    {
        public static void AddDependencyInjectionCustom(this IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjector.RegisterServices(services);
            DependencyInjector.AddDbContext<NorthwindContext>(configuration.GetConnectionString(nameof(NorthwindContext)));
        }
    }
}
