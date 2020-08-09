using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AuthServiceDomain.Entities
{
    public class UserModel : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
