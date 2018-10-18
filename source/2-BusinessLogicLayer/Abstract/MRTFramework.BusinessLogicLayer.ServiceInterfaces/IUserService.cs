using System.Collections.Generic;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.BusinessLogicLayer.ServiceInterfaces
{
    public interface IUserService
    {
        void Add(User user);
        List<User> GetAllUser(User user, string a, int b);
    }
}
