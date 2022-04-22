using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.SearchModels
{
    public class BaseSearch
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
