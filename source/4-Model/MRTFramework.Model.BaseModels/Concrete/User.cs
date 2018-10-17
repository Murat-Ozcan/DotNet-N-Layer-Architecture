using System;
using System.Collections.Generic;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.Model.BaseModels.Concrete
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
