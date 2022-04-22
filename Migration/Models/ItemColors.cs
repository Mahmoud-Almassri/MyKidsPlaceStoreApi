using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class ItemColors
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int ItemId { get; set; }
        public int Status { get; set; }

        public virtual Colour Color { get; set; }
        public virtual Item Item { get; set; }
    }
}
