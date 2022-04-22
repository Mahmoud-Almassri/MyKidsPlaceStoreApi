using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class UserClaims : IdentityUserClaim<long>
    {


        public virtual ApplicationUser User { get; set; }
    }
}
