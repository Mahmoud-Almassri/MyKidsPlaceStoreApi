using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Brand : BaseModel
    {
        public Brand()
        {
            Item = new HashSet<Item>();
        }

        public string BrandName { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }

    public  class BrandDTO : BaseModel
    {
        public BrandDTO()
        {
           
        }

        public string BrandName { get; set; }

    }
}
