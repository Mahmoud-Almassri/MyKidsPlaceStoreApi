using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class AspNetUserTokens
    {
        public long UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
