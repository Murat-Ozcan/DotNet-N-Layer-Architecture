using System.Collections.Generic;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.Model.BaseModels.Concrete
{
    public class Role : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
