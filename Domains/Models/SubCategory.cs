using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class SubCategory : BaseModel
    {
        public SubCategory()
        {
            Item = new HashSet<Item>();
        }

        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }
        public string NameAr { get; set; }
        public int? OrderNumber { get; set; }
        public bool? NoGenderClassification { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }

    public class SubCategoryDTO : BaseModel
    {
        public SubCategoryDTO()
        {
            
        }

        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }

    }
}
