using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Count { get; set; }
        public double? Price { get; set; }
        public int? OrderId { get; set; }

        public virtual UserCart Cart { get; set; }
        public virtual Item Item { get; set; }
        public virtual Orders Order { get; set; }
    }
}
