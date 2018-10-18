using System;
using System.Collections.Generic;
using System.Text;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.CrossCuttingConcern.Security;
using MRTFramework.DataAccessLayer.DAOInterfaces.Repositories;
using MRTFramework.Model.Requests;
using MRTFramework.Model.Responses;

namespace MRTFramework.BusinessLogicLayer.Domain.Managers
{
    public class AuthenticationManager: IAuthentication
    {
        private readonly IUserDao _userDao;
        private readonly IJsonWebToken _jsonWebToken;

        public AuthenticationManager(IUserDao userDao, IJsonWebToken jsonWebToken)
        {
            _userDao = userDao;
            _jsonWebToken = jsonWebToken;
        }

        public string Authenticate(LoginUserDto authentication)
        {
            var userLogin = _userDao.AuthenticationUser(authentication);
            return CreateJwt(userLogin);
        }

        private string CreateJwt(AuthenticationUserDto authenticated)
        {
            var sub = authenticated.Id.ToString();
            var rol = authenticated.Roles;
            return _jsonWebToken.Encode(sub, rol);
        }
    }
}
