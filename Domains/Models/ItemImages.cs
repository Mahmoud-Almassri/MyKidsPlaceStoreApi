using Domains.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Models
{
    public partial class ItemImages : BaseModel
    {
        public int ItemId { get; set; }
        public string ImagePath { get; set; }
        public bool IsDefault { get; set; }
        public virtual Item Item { get; set; }
    }

    public class ItemImagesDto : BaseModel
    {
        public int ItemId { get; set; }
        public string ImagePath { get; set; }
        public bool IsDefault { get; set; }
    }
}
