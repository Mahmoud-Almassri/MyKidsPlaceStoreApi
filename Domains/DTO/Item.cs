using Domains.Common;
using Domains.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.DTO
{
    public partial class ItemDTO 
    {
        

        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int Status { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public double Price { get; set; }
        public int ItemCount { get; set; }
        public bool IsSet { get; set; }
        public string Title { get; set; }
        public string DefaultImageUrl { get; set; }
    }
}
