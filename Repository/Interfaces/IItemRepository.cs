using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item> GetItemByIdWithRelatedAsync(int itemId);
        Task<BaseListResponse<ItemDTO>> GetFilteredItemsAsync(ItemSearch search);
        Task<List<ItemDTO>> GetItemsWithImagesAsync(int subCategoryId);
        bool Delete(int itemId);
        Task<BaseListResponse<ItemDTOs>> GetItemBySubCategoryWithSortAsync(int Id, int Gender, ItemSearch search);
        Task<List<ItemDTO>> GetItemsBySetIdAsync(int setId);
        Task<bool> UpdateItemsCountAsync(int setId, int count);
        Task<bool> UpdateItemAsync(Item item);
        Task<BaseListResponse<ItemDTOs>> GetItemsListAsync(ItemSearch search);
    }
}
