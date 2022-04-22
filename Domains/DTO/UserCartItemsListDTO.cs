using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTO
{
    public class UserCartItemsListDTO
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public double? SubTotal { get; set; }


        public double? OtherExpense { get; set; }
        public double? Delivery { get; set; }
        public double? Total { get; set; }

    }

   

}
