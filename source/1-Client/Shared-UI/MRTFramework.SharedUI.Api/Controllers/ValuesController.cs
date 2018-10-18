using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MRTFramework.BusinessLogicLayer.ServiceInterfaces;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.SharedUI.Api.Controllers
{
    //[Authorize(Roles = "Editor")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;

        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            var user = new User();
            return _userService.GetAllUser(user, "Murat", 10);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            User user = new User();

            UserRole userRole = new UserRole();
            userRole.RoleId = 1;

            user.Firstname = "Furkan";
            user.Surname = "Özcan";
            user.Age = 25;
            user.Birthday = new DateTime(1990, 5, 4);
            user.Email = "muratozcan@gmail.com";
            user.Password = "123456";
            user.UserRoles = new List<UserRole>();
            user.UserRoles.Add(userRole);
            _userService.Add(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
