using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class RoleClaims
    {
        public int Id { get; set; }
        public long RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual Roles Role { get; set; }
    }
}
