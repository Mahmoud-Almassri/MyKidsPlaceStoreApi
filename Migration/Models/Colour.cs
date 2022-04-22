using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Colour
    {
        public Colour()
        {
            CartItem = new HashSet<CartItem>();
            ItemColors = new HashSet<ItemColors>();
        }

        public int Id { get; set; }
        public string Color { get; set; }
        public int Status { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ItemColors> ItemColors { get; set; }
    }
}
