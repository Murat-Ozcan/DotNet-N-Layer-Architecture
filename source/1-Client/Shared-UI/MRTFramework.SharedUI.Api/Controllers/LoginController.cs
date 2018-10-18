using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.Model.Requests;

namespace MRTFramework.SharedUI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;

        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        public ActionResult Authenticate(LoginUserDto loginUserModel)
        {
            var deneme = _authentication.Authenticate(loginUserModel);

            return Ok(deneme);
        }
    }
}