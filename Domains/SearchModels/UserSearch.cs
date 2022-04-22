using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.SearchModels
{
    public partial class UserSearch : BaseSearch
    {
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }

    }
}
