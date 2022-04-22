using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class RoleClaims : IdentityRoleClaim<long>
    {


        public virtual Roles Role { get; set; }
    }
}
