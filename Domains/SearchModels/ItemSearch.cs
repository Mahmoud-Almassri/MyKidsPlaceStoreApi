using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.SearchModels
{
    public partial class ItemSearch : BaseSearch
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string SubCategoryName { get; set; }
        public int? Gender { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public int? SetId { get; set; }
        public int? Status { get; set; }
        public int? ItemId { get; set; }

    }
}
