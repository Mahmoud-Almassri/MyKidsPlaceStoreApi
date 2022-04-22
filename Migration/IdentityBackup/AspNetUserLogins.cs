using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class UserLogins//:IdentityUserLogin<long>
    {
       

        public virtual ApplicationUser User { get; set; }
    }
}
