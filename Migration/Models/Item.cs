using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class Item
    {
        public Item()
        {
            CartItem = new HashSet<CartItem>();
            ItemImages = new HashSet<ItemImages>();
        }

        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ItemCount { get; set; }
        public bool IsSet { get; set; }
        public int? SetId { get; set; }
        public int? ItemOrder { get; set; }
        public int Gender { get; set; }
        public string DescriptionAr { get; set; }
        public int? SaleId { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Set Set { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ItemImages> ItemImages { get; set; }
    }
}
