using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public int Status { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
