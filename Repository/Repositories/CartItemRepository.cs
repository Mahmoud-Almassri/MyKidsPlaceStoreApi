using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private MyKidsStoreDbContext _context;
        public CartItemRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public UserCart GetCartItemByUserId(long userId)
        {
            return _context.UserCart.FirstOrDefault(x=>x.UserId == userId);
        }

        public async Task<IList<CartItemDto>> GetLoggedInUserOrders(long userId)
        {
            return await _context.CartItem.Where(x => x.Cart.UserId == userId && x.Status == (int)GlobalStatusEnum.InCart)
                                                .Include(i => i.Item)
                                                .ThenInclude(y => y.ItemImages)
                                                .Include("Item.Brand")
                                                .Include("Item.SubCategory")
                                                .Select(x => new CartItemDto
                                                {
                                                    Id = x.Id,
                                                    CartId = x.CartId,
                                                    ItemId = x.ItemId,
                                                    OrderId = x.OrderId,
                                                    Price = x.Price,
                                                    Status = x.Status,
                                                    Count = x.Count,
                                                    CreatedDate = x.CreatedDate,
                                                    Item = new ItemDTOs
                                                    {
                                                        Brand = new BrandDTO
                                                        {
                                                            Id = x.Item.Brand.Id,

                                                            BrandName = x.Item.Brand.BrandName,
                                                            Status = x.Item.Brand.Status
                                                        },
                                                        BrandId = x.Item.BrandId,
                                                        Gender = x.Item.Gender,
                                                        ItemCount = x.Item.ItemCount,
                                                        Id = x.Item.Id,
                                                        IsSet = x.Item.IsSet,
                                                        ItemImages = x.Item.ItemImages.Select(I => new ItemImagesDto
                                                        {
                                                            Id = I.Id,
                                                            ImagePath = I.ImagePath,
                                                            Status = I.Status,
                                                            IsDefault = I.IsDefault,
                                                            ItemId = I.ItemId,
                                                        }).ToList(),
                                                        SaleId = x.Item.SaleId,
                                                        Status = x.Item.Status,
                                                        Title = x.Item.Title,
                                                        SubCategory = new SubCategoryDTO
                                                        {
                                                            Id = x.Item.SubCategory.Id,
                                                            Name = x.Item.SubCategory.Name,
                                                            NameAr = x.Item.SubCategory.NameAr,
                                                            CategoryId = x.Item.SubCategory.CategoryId,
                                                            Description = x.Item.SubCategory.Description,
                                                            CreatedDate = x.Item.SubCategory.CreatedDate,
                                                            ModifiedDate = x.Item.SubCategory.ModifiedDate,
                                                            Status = x.Item.SubCategory.Status
                                                        },
                                                        SubCategoryId = x.Item.SubCategoryId,
                                                        Price = x.Item.Price,
                                                        SetId = x.Item.SetId,
                                                        ItemOrder = x.Item.ItemOrder

                                                    }
                                                })
                                                .ToListAsync();
        }
    }
}
