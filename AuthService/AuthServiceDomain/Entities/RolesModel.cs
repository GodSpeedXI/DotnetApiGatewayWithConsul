using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthServiceDomain.Entities
{
    public class RolesModel : IdentityRole<int>
    {
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
