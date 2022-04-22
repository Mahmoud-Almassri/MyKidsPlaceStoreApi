using Domains.Models;
using System;
using System.Collections.Generic;

namespace Domains.DTO
{
    public partial class OrderDTO
    {


        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public double TotalAmount { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
        public UserDTO CreatedByNavigation { get; set; }

        public IList<CartItemDTO> CartItem { get; set; }
    }
}
