using MRTFramework.Model.Requests;

namespace MRTFramework.BusinessLogicLayer.ServiceInterfaces
{
    public interface IAuthentication
    {
        string Authenticate(LoginUserDto authentication);
    }
}
