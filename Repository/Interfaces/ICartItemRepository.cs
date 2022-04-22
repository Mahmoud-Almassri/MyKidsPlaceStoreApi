using Domains.DTO;
using Domains.Models;
using Repository.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        UserCart GetCartItemByUserId(long userId);
        Task<IList<CartItemDto>> GetLoggedInUserOrders(long userId);
    }
}
