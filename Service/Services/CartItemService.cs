using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Domains.SearchModels;
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
    public class CartItemService : ICartItemService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly LoggedInUserService _loggedInUserService;
        public CartItemService(IRepositoryUnitOfWork repositoryUnitOfWork, LoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<CartItem> AddAsync(CartItem entity)
        {
            entity.CartId = GetLoggedInUserCart();
            //bool isItemExistInCart = CheckIsItemExistInActiveCart(entity.ItemId, entity.CartId);
            entity.Price = await GetItemPriceAsync(entity.ItemId) * entity.Count;
            entity.Status = (int)GlobalStatusEnum.InCart;
            entity.CreatedDate = DateTime.Now;
            return await _repositoryUnitOfWork.CartItem.Value.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task<bool> FixOrderItemsAsync(int orderId) {
            var cartItems = _repositoryUnitOfWork.CartItem.Value.Find(x => x.OrderId == orderId).ToList();
            var order = await _repositoryUnitOfWork.Order.Value.FirstOrDefault(x => x.Id == orderId);

            foreach(var cartItem in cartItems)
            {
                cartItem.Price = await GetItemPriceAsync(cartItem.ItemId) * cartItem.Count;
               
            }
            order.TotalAmount = (double)cartItems.Sum(x => x.Price);

            await _repositoryUnitOfWork.CartItem.Value.UpdateRangeAsync(cartItems);
           
            await _repositoryUnitOfWork.Order.Value.UpdateAsync(order);
            return true;
        }

        private async System.Threading.Tasks.Task<double> GetItemPriceAsync(int itemId)
        {
            Item item = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.Id == itemId, i => i.Sale);

            if (item != null)
            {
                if (item.SaleId > 0)
                {
                    if(item.Sale.EndDate >= DateTime.Now && item.Sale.Status == GlobalStatusEnum.Active.GetHashCode())
                    {
                        return item.Sale.NewPrice;
                    }
                    else
                    {
                        return item.Price;
                    }
                }
                else
                {
                    return item.Price;
                }
            }
            return 0;
        }

        private bool CheckIsItemExistInActiveCart(int itemId, int cartId)
        {
            if(_repositoryUnitOfWork.CartItem.Value.Any(x => x.ItemId == itemId && x.CartId == cartId && x.Status == (int)GlobalStatusEnum.InCart))
            {
                throw new ValidationException(ErrorMessages.ItemAlreadyExist.GetHashCode().ToString());
            }
            return false;
        }
        private int GetLoggedInUserCart()
        {
            long userId = _loggedInUserService.GetUserId();
            UserCart cart = _repositoryUnitOfWork.CartItem.Value.GetCartItemByUserId(userId);
            if (cart == null)
            {
                throw new ValidationException(ErrorMessages.UserCartNotFound.GetHashCode().ToString());
            }
            return cart.Id;
        }

        public IEnumerable<CartItem> AddRange(IEnumerable<CartItem> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<UserCartItemsListDTO> GetLoggedInUserOrdersAsync() {
            long userId = _loggedInUserService.GetUserId();
            var cartItems = await _repositoryUnitOfWork.CartItem.Value.GetLoggedInUserOrders(userId);
            double? totalPrices = cartItems.Sum(x => x.Price);
            double? Delivery = 2;
            double? Others = 0;
            UserCartItemsListDTO userCart = new UserCartItemsListDTO
            {
                CartItems = cartItems,
                Delivery = cartItems.Any()? Delivery : 0,
                OtherExpense = cartItems.Any() ? Others : 0,
                SubTotal = totalPrices.HasValue ? totalPrices : 0,
                Total = totalPrices.HasValue || totalPrices != 0 ? totalPrices + Delivery + Others : 0 + Delivery + Others,
            };
            return userCart;
        }

        public async Task<UserCartItemsListDTO> UpdateCartItemAsync(CartItem entity)
        {
            entity.Price = await GetItemPriceAsync(entity.ItemId) * entity.Count;
            await _repositoryUnitOfWork.CartItem.Value.UpdateAsync(entity);
            return await GetLoggedInUserOrdersAsync();
        }

        public async System.Threading.Tasks.Task<UserCartItemsListDTO> UpdateCartItemCountAsync (int cartItemId, int count)
        {
            CartItem cartItem = await _repositoryUnitOfWork.CartItem.Value.FirstOrDefault(x=>x.Id == cartItemId, i=>i.Item);
            if(count > cartItem.Item.ItemCount)
            {
                throw new ValidationException(ErrorMessages.ItemCountNotEnough.GetHashCode().ToString());
            }
            cartItem.Count = count;
            cartItem.Price = await  GetItemPriceAsync(cartItem.ItemId) * count;
            await _repositoryUnitOfWork.CartItem.Value.UpdateAsync(cartItem);
            return await GetLoggedInUserOrdersAsync();
        }

        public async System.Threading.Tasks.Task<CartItem> GetAsync(int Id)
        {
            CartItem cartItem = await _repositoryUnitOfWork.CartItem.Value.GetAsync(Id);
            return cartItem;
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _repositoryUnitOfWork.CartItem.Value.GetAll();
        }

        public async System.Threading.Tasks.Task<BaseListResponse<CartItem>> ListAsync(BaseSearch search)
        {
            BaseListResponse<CartItem> CartItems = await _repositoryUnitOfWork.CartItem.Value.List(x=> true, search.PageSize, search.PageNumber);
            return CartItems;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.CartItem.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<CartItem> RemoveRange(IEnumerable<CartItem> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItem> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

      

        public IEnumerable<CartItem> UpdateRange(IEnumerable<CartItem> entities)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> UpdateAsync(CartItem entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
