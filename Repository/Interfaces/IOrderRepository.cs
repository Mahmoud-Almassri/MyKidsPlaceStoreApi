using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Orders>
    {
        Task<IList<Orders>> GetLoggedInUserOrdersAsync(long userId);
        OrderDTO GetOrderByIdWithRelated(int orderId);
        IEnumerable<Orders> FixAllOrdersAmount();
        List<UserDTO> GetSubCategoryUsers(int subCategoryId);
        BaseListResponse<OrderDTO> GetOrdersList(OrderSearch search);
    }

}
