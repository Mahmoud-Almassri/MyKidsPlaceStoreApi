using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICartItemService : IService<CartItem, BaseSearch>
    {
        Task<UserCartItemsListDTO> GetLoggedInUserOrdersAsync();
        Task<UserCartItemsListDTO> UpdateCartItemAsync(CartItem cartItem);
        Task<UserCartItemsListDTO> UpdateCartItemCountAsync(int cartItemId, int count);
        Task<bool> FixOrderItemsAsync(int orderId);
    }
}
