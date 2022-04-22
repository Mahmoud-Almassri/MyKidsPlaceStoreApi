using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class UserCart : BaseModel
    {
        public UserCart()
        {
            CartItem = new HashSet<CartItem>();
        }

        public long UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
