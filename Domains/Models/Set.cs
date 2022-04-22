using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Set : BaseModel
    {
        public Set()
        {
            Item = new HashSet<Item>();
        }
        public string SetName { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
