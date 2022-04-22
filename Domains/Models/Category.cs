using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Category : BaseModel
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public string CategoryNameAr { get; set; }

        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}
