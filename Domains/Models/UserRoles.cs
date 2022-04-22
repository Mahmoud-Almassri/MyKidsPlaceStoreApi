using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class UserRoles : IdentityUserRole<long>
    {
        public virtual Roles Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
