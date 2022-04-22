using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Set
    {
        public Set()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public string SetName { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
