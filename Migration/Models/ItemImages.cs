using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class ItemImages
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ImagePath { get; set; }
        public int Status { get; set; }
        public bool IsDefault { get; set; }

        public virtual Item Item { get; set; }
    }
}
