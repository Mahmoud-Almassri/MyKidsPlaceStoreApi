using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Domains.SearchModels;
using Repository.Context;
using Repository.Interfaces.Common;
using Repository.UnitOfWork;
using Service.Interfaces;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly LoggedInUserService _loggedInUserService;
        private readonly PushNotificationService _pushNotificationService;
        private MyKidsStoreDbContext _context;
        public OrderService(
            IRepositoryUnitOfWork repositoryUnitOfWork,
            PushNotificationService pushNotificationService,
            MyKidsStoreDbContext context,
              LoggedInUserService loggedInUserService
            )
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
            _context = context;
            _pushNotificationService = pushNotificationService;
            _loggedInUserService = loggedInUserService;
        }

        public async System.Threading.Tasks.Task<bool> AddOrdera()
        {
            List<ApplicationUser> applicationUsers = _context.ApplicationUser.Where(x => x.UserCart.FirstOrDefault().CartItem.Any(x => x.Status == 3)).ToList();
            foreach(var user in applicationUsers)
            {
                Orders entity = new Orders();
                long UserId = user.Id;

                entity.Status = (int)GlobalStatusEnum.Active;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = UserId;
                List<CartItem> cartItems = _repositoryUnitOfWork.CartItem.Value.Find(x => x.Cart.UserId == UserId && x.Cart.User.IsActive == true && x.Status == (int)GlobalStatusEnum.InCart).ToList();
                if (cartItems.Count > 0)
                {
                    entity.TotalAmount = (double)cartItems.Sum(x => x.Price);

                }
                else
                {
                    continue;
                    //throw new ValidationException(ErrorMessages.CartIsEmpty.GetHashCode().ToString());
                }
                await _repositoryUnitOfWork.Order.Value.AddAsync(entity);
                //Send notification to admin
                await _pushNotificationService.SendNotificationToAdminAsync(new Domains.Common.PushNotification
                {
                    Body = "New order has been received",
                    Title = "New order !"
                });
                foreach (CartItem cartItem in cartItems)
                {
                    Item item = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.Id == cartItem.ItemId);
                    if (item.ItemCount <= 0)
                    {
                        await _repositoryUnitOfWork.CartItem.Value.RemoveAsync(cartItem.Id);
                        entity.TotalAmount = entity.TotalAmount - (double)cartItem.Price;
                        await _repositoryUnitOfWork.Order.Value.UpdateAsync(entity);
                        continue;
                    }

                    cartItem.Status = (int)GlobalStatusEnum.InMyOrders;

                    item.ItemCount = item.ItemCount - cartItem.Count;

                    if (item.IsSet)
                    {
                        await _repositoryUnitOfWork.Item.Value.UpdateItemsCountAsync(Convert.ToInt32(item.SetId), cartItem.Count);
                    }
                    cartItem.OrderId = entity.Id;
                    if (item.ItemCount <= 0)
                    {
                        item.Status = (int)GlobalStatusEnum.Empty;
                        await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(item);

                        await RemoveOtherUsersCartItemsAsync(item.Id, cartItem.Id);
                        await checkIsSetItemAndRemoveAsync(item);
                    }
                    else
                    {
                        await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(item);
                    }
                    await _repositoryUnitOfWork.CartItem.Value.UpdateAsync(cartItem);

                }
                await _pushNotificationService.SendNotificationToUserAsync(UserId, new Domains.Common.PushNotification
                {
                    Body = "Your order has been received",
                    Title = "New order !"
                });
                
            }
            return true;
        }
        public async System.Threading.Tasks.Task<Orders> AddOrder(Orders entity)
        {
            
            long UserId = _loggedInUserService.GetUserId();
            
            entity.Status = (int)GlobalStatusEnum.Active;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = UserId;
            List<CartItem> cartItems = _repositoryUnitOfWork.CartItem.Value.Find(x => x.Cart.UserId == UserId && x.Cart.User.IsActive == true && x.Status == (int)GlobalStatusEnum.InCart).ToList();
            if(cartItems.Count > 0)
            {
                entity.TotalAmount = (double) cartItems.Sum(x => x.Price);

            }
            else
            {
                throw new ValidationException(ErrorMessages.CartIsEmpty.GetHashCode().ToString());
            }
            await _repositoryUnitOfWork.Order.Value.AddAsync(entity);
            //Send notification to admin
            await _pushNotificationService.SendNotificationToAdminAsync(new Domains.Common.PushNotification
            {
                Body = "New order has been received",
                Title = "New order !"
            });
            foreach (CartItem cartItem in cartItems)
            {
                Item item = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.Id == cartItem.ItemId);
                if (item.ItemCount <= 0)
                {
                    await _repositoryUnitOfWork.CartItem.Value.RemoveAsync(cartItem.Id);
                    entity.TotalAmount = entity.TotalAmount - (double)cartItem.Price;
                    await _repositoryUnitOfWork.Order.Value.UpdateAsync(entity);
                    continue;
                }
               
                cartItem.Status = (int)GlobalStatusEnum.InMyOrders;
             
                item.ItemCount = item.ItemCount - cartItem.Count;
                
                if (item.IsSet)
                {
                    await _repositoryUnitOfWork.Item.Value.UpdateItemsCountAsync(Convert.ToInt32(item.SetId), cartItem.Count);
                }
                cartItem.OrderId = entity.Id;
                if(item.ItemCount <= 0)
                {
                    item.Status = (int) GlobalStatusEnum.Empty;
                    await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(item);
             
                    await RemoveOtherUsersCartItemsAsync(item.Id, cartItem.Id);
                    await checkIsSetItemAndRemoveAsync(item);
                }
                else
                {
                    await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(item);
                }
                await _repositoryUnitOfWork.CartItem.Value.UpdateAsync(cartItem);
       
            }
            await _pushNotificationService.SendNotificationToUserAsync(UserId, new Domains.Common.PushNotification
            {
                Body = "Your order has been received",
                Title = "New order !"
            });
            return entity;
        }

        private async Task checkIsSetItemAndRemoveAsync(Item Item)
        {
            if (Item != null)
            {
                if (Item.SetId.HasValue)
                {
                    if (!Item.IsSet)
                    {
                        var setItem = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.SetId == Item.SetId && x.IsSet == true);
                        if(setItem != null)
                        {
                            setItem.Status = GlobalStatusEnum.Empty.GetHashCode();
                            await _repositoryUnitOfWork.Item.Value.UpdateItemAsync(setItem);
                        }
                       
                    }
                }
            }
            //var setItem = _repositoryUnitOfWork.Item.Value.FirstOrDefault(x=>x.)

        }
        public async System.Threading.Tasks.Task<bool> RemoveOtherUsersCartItemsAsync(int itemId, int cartItemId)
        {
            List<CartItem> cartItems = _repositoryUnitOfWork.CartItem.Value.Find(x => x.ItemId == itemId && x.Id != cartItemId && x.Status == (int)GlobalStatusEnum.InCart, i => i.Cart).ToList();
            foreach(CartItem item in cartItems)
            {

                //Send notification to other users
                await _pushNotificationService.SendNotificationToUserAsync(item.Cart.UserId, new Domains.Common.PushNotification
                {
                    Body = "The item in your cart is out of store",
                    Title = "we are sorry!"
                });
                //Send notification to other users
                await _pushNotificationService.SendNotificationToUserAsync(item.Cart.UserId, new Domains.Common.PushNotification
                {
                    Body = "لقد نفذ الصنف الموجود في سلتك من المتجر",
                    Title = "آسفون"
                });
                item.Cart = null;
                //remove from cart item
                await _repositoryUnitOfWork.CartItem.Value.RemoveAsync(item.Id);
            }
            
          
            return true;
        }
        public async Task<Orders> UpdateAsync(Orders entity)
        {

            return await _repositoryUnitOfWork.Order.Value.UpdateAsync(entity);
        }
        public List<UserDTO> GetSubCategoryUsers(int subCategoryId)
        {
            List<UserDTO> users=_repositoryUnitOfWork.Order.Value.GetSubCategoryUsers(subCategoryId);
            return users;
        }

        public OrderDTO GetWithRelated(int Id)
        {
            OrderDTO Order = _repositoryUnitOfWork.Order.Value.GetOrderByIdWithRelated(Id);
            return Order;
        } 
        public async Task<Orders> GetAsync(int Id)
        {
            Orders Order = await _repositoryUnitOfWork.Order.Value.GetAsync(Id);
            return Order;
        }
      
        public BaseListResponse<OrderDTO> ListOrders(OrderSearch search)
        {
            //BaseListResponse<Orders> Orders = _repositoryUnitOfWork.Order.Value.List(x =>
            //             (!search.Id.HasValue || x.Id==search.Id) &&
            //             (!search.FromDate.HasValue || x.CreatedDate>=search.FromDate) &&
            //             (!search.ToDate.HasValue || x.CreatedDate<=search.ToDate) &&
            //             (search.IsActiveOrders ? x.CreatedDate.Value.Date==DateTime.Now.Date: x.CreatedDate.Value.Date <= DateTime.Now.Date) &&
            //             (!search.TotalAmount.HasValue || x.TotalAmount==search.TotalAmount), 
            //search.PageSize, search.PageNumber,x=>x.CreatedByNavigation);
            BaseListResponse<OrderDTO> Orders = _repositoryUnitOfWork.Order.Value.GetOrdersList(search);
            return Orders;
        }
         public async Task<IList<Orders>> GetLoggedInUserOrdersAsync()
        {
            long userId = _loggedInUserService.GetUserId();
            IList<Orders> Orders = await _repositoryUnitOfWork.Order.Value.GetLoggedInUserOrdersAsync(userId);
            return Orders;
        } 
        public IEnumerable<Orders> FixAllOrdersAmount()
        {
            IEnumerable<Orders> Orders = _repositoryUnitOfWork.Order.Value.FixAllOrdersAmount();
            return Orders;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Order.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<Orders> AddRange(IEnumerable<Orders> entities)
        {
            throw new NotImplementedException();
        }

       
        public IEnumerable<Orders> GetAll()
        {
            return _repositoryUnitOfWork.Order.Value.GetAll();
        }

        

        

        public IEnumerable<Orders> RemoveRange(IEnumerable<Orders> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orders> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Orders> UpdateRange(IEnumerable<Orders> entities)
        {
            throw new NotImplementedException();
        }

        public Orders AddAsync(Orders entity)
        {
            throw new NotImplementedException();
        }

        public BaseListResponse<Orders> ListAsync(OrderSearch entity)
        {
            throw new NotImplementedException();
        }

        Task<Orders> IService<Orders, OrderSearch>.AddAsync(Orders entity)
        {
            throw new NotImplementedException();
        }

        Task<BaseListResponse<Orders>> IService<Orders, OrderSearch>.ListAsync(OrderSearch entity)
        {
            throw new NotImplementedException();
        }
    }
}
