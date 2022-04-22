using System;

namespace Domains.DTO
{
    public partial class CartItemDTO
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Count { get; set; }
        public double? Price { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public int? OrderId { get; set; }

        public ItemDTO Item { get; set; }
    }
}
