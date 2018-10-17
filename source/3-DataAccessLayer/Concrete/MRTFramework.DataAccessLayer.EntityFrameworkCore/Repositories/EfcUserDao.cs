using System.Linq;
using Microsoft.EntityFrameworkCore;
using MRTFramework.CrossCuttingConcern.EntityFrameworkCore;
using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.DataAccessLayer.EntityFrameworkCore.Context;
using MRTFramework.Model.BaseModels.Concrete;
using MRTFramework.Model.Requests;
using MRTFramework.Model.Responses;

namespace MRTFramework.DataAccessLayer.EntityFrameworkCore.Repositories
{
    public class EfcUserDao : EntityFrameworkCoreRepository<User, NorthwindContext>, IUserDao
    {
        public EfcUserDao(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public AuthenticationUserDto AuthenticationUser(LoginUserDto authenticationUser)
        {
            //Other Method
            //var userAuth = DbSet.Where(x =>
            //        x.Email == authenticationUser.Email && x.Password == authenticationUser.Password)
            //    .SelectMany(a => a.UserRoles).Include(b => b.Role).GroupBy(t => t.UserId).Select(c => new AuthenticationUserModel
            //    {
            //        Id = c.Key,
            //        Roles = c.Select(e => e.Role.Name).ToArray()
            //    }).FirstOrDefault();

            var userAuth = DbSet.Where(x =>
                    x.Email == authenticationUser.Email && x.Password == authenticationUser.Password)
                .SelectMany(a => a.UserRoles).Include(b => b.Role).Select(q => new { q.User.Id, q.Role.Name }).ToList();

            var result = userAuth.GroupBy(a => a.Id).Select(a => new AuthenticationUserDto
            {
                Id = a.Key,
                Roles = a.Select(b => b.Name).ToArray()
            }).FirstOrDefault();

            return result;
        }
    }
}
