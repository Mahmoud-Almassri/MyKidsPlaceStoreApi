using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class UserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public long UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
