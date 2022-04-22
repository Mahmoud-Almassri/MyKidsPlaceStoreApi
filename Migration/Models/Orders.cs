using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Orders
    {
        public Orders()
        {
            CartItem = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public double TotalAmount { get; set; }

        public virtual ApplicationUser CreatedByNavigation { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
