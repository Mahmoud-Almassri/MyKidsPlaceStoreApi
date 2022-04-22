using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
