using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class UserCart
    {
        public UserCart()
        {
            CartItem = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        public int Status { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
