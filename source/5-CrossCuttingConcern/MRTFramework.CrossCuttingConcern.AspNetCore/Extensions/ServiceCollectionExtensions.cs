using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using MRTFramework.CrossCuttingConcern.DependencyInjection;
using MRTFramework.CrossCuttingConcern.Security;

namespace MRTFramework.CrossCuttingConcern.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthenticationCustom(this IServiceCollection services)
        {
            var jsonWebToken = DependencyInjector.GetService<IJsonWebToken>();

            void JwtBearer(JwtBearerOptions jwtBearer)
            {
                jwtBearer.TokenValidationParameters = jsonWebToken.TokenValidationParameters;
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
        }
    }
}
