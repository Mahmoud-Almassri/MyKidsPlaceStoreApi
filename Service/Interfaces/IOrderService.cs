using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Service.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrderService : IService<Orders, OrderSearch>
    {

        Task<IList<Orders>> GetLoggedInUserOrdersAsync();
        IEnumerable<Orders> FixAllOrdersAmount();
        System.Threading.Tasks.Task<Orders> AddOrder(Orders entity);
        System.Threading.Tasks.Task<bool> AddOrdera();
        OrderDTO GetWithRelated(int Id);
        List<UserDTO> GetSubCategoryUsers(int subCategoryId);
        BaseListResponse<OrderDTO> ListOrders(OrderSearch search);
    }
}
