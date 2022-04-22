using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
        public string ImagePath { get; set; }
        public string CategoryNameAr { get; set; }

        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}
