using Domains.Common;
using System;
using System.Collections.Generic;

namespace Domains.Models
{
    public partial class News : BaseModel
    {
        public News()
        {
        }

        public string NewsDescription { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
