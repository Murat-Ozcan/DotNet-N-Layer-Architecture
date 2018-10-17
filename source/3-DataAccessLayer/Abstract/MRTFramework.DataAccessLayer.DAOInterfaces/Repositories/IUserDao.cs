using MRTFramework.CrossCuttingConcern.Utils.Interfaces;
using MRTFramework.Model.BaseModels.Concrete;
using MRTFramework.Model.Requests;
using MRTFramework.Model.Responses;

namespace MRTFramework.DataAccessLayer.DAOInterfaces.Repositories
{
    public interface IUserDao : ISyncRepository<User>, IAsyncRepository<User>
    {
        AuthenticationUserDto AuthenticationUser(LoginUserDto authenticationUser);
    }
}
