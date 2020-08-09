using Microsoft.AspNetCore.Identity;

namespace AuthServiceDomain.Entities
{
    public class UserRoleModel : IdentityUserRole<int>
    {
        public virtual RolesModel Role { get; set; }
        public virtual UserModel User { get; set; }
    }
}
