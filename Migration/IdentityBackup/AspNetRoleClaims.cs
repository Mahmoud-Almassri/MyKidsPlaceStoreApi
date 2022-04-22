using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class ApplicationRoleClaims
    {

        public virtual Roles Role { get; set; }
    }
}
