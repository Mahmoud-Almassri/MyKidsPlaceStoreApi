using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class Orders: BaseModel
    {
        public Orders()
        {
            CartItem = new HashSet<CartItem>();
        }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public double TotalAmount { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
    }

    public class OrdersDTO : BaseModel
    {
        public OrdersDTO()
        {

        }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public double TotalAmount { get; set; }

       
       
    }
}
