using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Sale: BaseModel
    {
        public Sale()
        {
   
            Items = new HashSet<Item>();
        }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
    public partial class SaleDTO: BaseModel
    {
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    } 
    
    public partial class SignleSaleDTO: BaseModel
    {
        public double OldPrice { get; set; }
        public int ItemId { get; set; }
        public double NewPrice { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
