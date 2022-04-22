using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RolesClaims = new HashSet<ApplicationRoleClaims>();
            UserRoles = new HashSet<UserRoles>();
        }


        public virtual ICollection<ApplicationRoleClaims> RolesClaims { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
