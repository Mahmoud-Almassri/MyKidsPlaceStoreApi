using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class ItemSizes
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ItemId { get; set; }
        public int Status { get; set; }

        public virtual Item Item { get; set; }
        public virtual Size Size { get; set; }
    }
}
