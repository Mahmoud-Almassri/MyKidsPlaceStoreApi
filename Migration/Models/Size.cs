using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Size
    {
        public Size()
        {
            CartItem = new HashSet<CartItem>();
            ItemSizes = new HashSet<ItemSizes>();
        }

        public int Id { get; set; }
        public string Size1 { get; set; }
        public int Status { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ItemSizes> ItemSizes { get; set; }
    }
}
