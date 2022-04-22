using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : Repository<Orders>, IOrderRepository
    {
        private MyKidsStoreDbContext _context;
        public OrderRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Orders>> GetLoggedInUserOrdersAsync(long userId)
        {
            IList<Orders> orders = await _context.Orders
                                                 .Where(x => x.CreatedBy == userId)
                                                 .Include(x => x.CartItem)
                                                 .Include("CartItem.Item")
                                                 .Include("CartItem.Item.ItemImages")
                                                 .Include("CartItem.Item.SubCategory")
                                                 .OrderByDescending(x => x.CreatedDate)
                                                 .ToListAsync();
            return orders;
        }
        public IEnumerable<Orders> FixAllOrdersAmount()
        {
            List<Orders> orders = _context.Orders
                                                 .Include(x => x.CartItem)
                                                 .OrderByDescending(x => x.CreatedDate).ToList();
            foreach (var order in orders)
            {
                order.TotalAmount = (double)order.CartItem.Sum(x => x.Price);
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();

            }
            return orders;
        }

        //public Orders FixOrderAmount(int orderId)
        //{
        //    Orders orders = _context.Orders
        //                                         .Include(x => x.CartItem)
        //                                         .FirstOrDefault(x => x.Id == orderId);
        //    foreach (var order in orders)
        //    {
        //        order.TotalAmount = (double)order.CartItem.Sum(x => x.Price);
        //        _context.Entry(order).State = EntityState.Modified;
        //        _context.SaveChanges();

        //    }
        //    return orders;
        //}
        public List<UserDTO> GetSubCategoryUsers(int subCategoryId)
        {
            List<UserDTO> users = _context.CartItem.Include(x => x.Item).Include(x => x.Cart).ThenInclude(x => x.User).Where(x => x.Item.SubCategoryId == subCategoryId).Select(x => new UserDTO
            {
                Id = x.Cart.User.Id,
                Address = x.Cart.User.Address,
                FullName = x.Cart.User.FullName,
                MobileNumber = x.Cart.User.MobileNumber
            }).Distinct().ToList();

            return users;
        }
        public OrderDTO GetOrderByIdWithRelated(int orderId)
        {
            return _context.Orders.Where(x => x.Id == orderId)
                                .Include(x => x.CartItem)
                                .Include("CartItem.Item")
                                .Include("CartItem.Item.Sale")
                                .Include("CartItem.Item.Brand")
                                .Include("CartItem.Item.SubCategory")
                                .Include("CartItem.Item.SubCategory.Category")
                                .Include("CartItem.Item.Set")
                                //.Include("CartItem.Item.ItemColors")
                                //.Include("CartItem.Item.ItemColors.Color")
                                //.Include("CartItem.Item.ItemSizes")
                                //.Include("CartItem.Item.ItemSizes.Size")
                                .Select(x => new OrderDTO
                                {
                                    CartItem = x.CartItem.Select(c => new CartItemDTO
                                    {
                                        CartId = c.CartId,
                                        Count = c.Count,
                                        CreatedDate = c.CreatedDate,
                                        Id = c.Id,
                                        Item = new ItemDTO
                                        {
                                            Id = c.Item.Id,
                                            BrandId = c.Item.BrandId,
                                            BrandName = c.Item.Brand.BrandName,
                                            CategoryName = c.Item.SubCategory.Category.CategoryName,
                                            DefaultImageUrl = c.Item.ItemImages.Where(s => s.IsDefault).FirstOrDefault().ImagePath,
                                            IsSet = c.Item.IsSet,
                                            ItemCount = c.Item.ItemCount,
                                            Price = c.Item.Price,
                                            SubCategoryId = c.Item.SubCategoryId,
                                            SubCategoryName = c.Item.SubCategory.Name,
                                            Status = c.Item.Status,
                                        },
                                        ItemId = c.ItemId,
                                        OrderId = c.OrderId,
                                        Price = c.Price,
                                        Status = c.Status
                                    }).ToList(),
                                    CreatedDate = x.CreatedDate,
                                    TotalAmount = x.TotalAmount,
                                    Status = x.Status,
                                    Id = x.Id,
                                    CreatedBy = x.CreatedBy
                                })
                                .FirstOrDefault();
        }
        public BaseListResponse<OrderDTO> GetOrdersList(OrderSearch search)
        {
            var orders= _context.Orders.Where(x => (!search.Id.HasValue || x.Id == search.Id) &&
                         (!search.FromDate.HasValue || x.CreatedDate >= search.FromDate) &&
                         (!search.ToDate.HasValue || x.CreatedDate <= search.ToDate) &&
                         (search.IsActiveOrders ? x.CreatedDate.Value.Date == DateTime.Now.Date : x.CreatedDate.Value.Date <= DateTime.Now.Date) &&
                         (!search.TotalAmount.HasValue || x.TotalAmount == search.TotalAmount))
                                .Include(x => x.CreatedByNavigation)
                                .OrderByDescending(x => x.Id)
                                .Select(x => new OrderDTO
                                {
                                   CreatedDate = x.CreatedDate,
                                    TotalAmount = x.TotalAmount,
                                    Status = x.Status,
                                    Id = x.Id,
                                    CreatedBy = x.CreatedBy,
                                    CreatedByNavigation=new UserDTO {
                                    Address=x.CreatedByNavigation.Address,
                                    FullName=x.CreatedByNavigation.FullName,
                                    Id=x.CreatedByNavigation.Id,
                                    IsActive=x.CreatedByNavigation.IsActive,
                                    MobileNumber=x.CreatedByNavigation.MobileNumber
                                }
                                })
                                .ToList();
            return new BaseListResponse<OrderDTO>
            {
                entities = orders.Skip(search.PageSize * (search.PageNumber - 1))
                                .Take(search.PageSize).ToList(),
                TotalCount = orders.Count
            };
        }
    }

}


