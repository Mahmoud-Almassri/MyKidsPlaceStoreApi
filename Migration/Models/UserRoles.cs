using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class UserRoles
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
