using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.SearchModels
{
    public partial class OrderSearch : BaseSearch
    {
        public long? Id { get; set; }
        public double? TotalAmount { get; set; }
        public bool IsActiveOrders { get; set; }

    }
}
