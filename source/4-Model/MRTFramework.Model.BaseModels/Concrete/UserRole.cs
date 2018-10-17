using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.Model.BaseModels.Concrete
{
    public class UserRole : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
