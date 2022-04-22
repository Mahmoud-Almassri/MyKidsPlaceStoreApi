using Domains.Common;
using Domains.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Models
{
    public partial class Item : BaseModel
    {
        public Item()
        {
            CartItem = new HashSet<CartItem>();
            ItemImages = new HashSet<ItemImages>();
        }

        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int ItemCount { get; set; }
        public bool IsSet { get; set; }
        public int? SetId { get; set; }
        public int? SaleId { get; set; }
        public int Gender { get; set; }
        public int? ItemOrder { get; set; }
        [NotMapped]
        public SetDTO SetDTO { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Set Set { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<ItemImages> ItemImages { get; set; }
        public virtual Sale Sale { get; set; }
    }


    public partial class ItemDTOs : BaseModel
    {
        public ItemDTOs()
        {
            CartItem = new HashSet<CartItemDTO>();
            ItemImages = new HashSet<ItemImagesDto>();
        }

        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int ItemCount { get; set; }
        public bool IsSet { get; set; }
        public int? SetId { get; set; }
        public int? SaleId { get; set; }
        public int Gender { get; set; }
        public int? ItemOrder { get; set; }
        [NotMapped]
        public SetDTO SetDTO { get; set; }
        public virtual BrandDTO Brand { get; set; }
        public virtual SubCategoryDTO SubCategory { get; set; }
        public virtual ICollection<CartItemDTO> CartItem { get; set; }
        public virtual ICollection<ItemImagesDto> ItemImages { get; set; }
        public virtual SaleDTO Sale { get; set; }
    }
}
