using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Service.Interfaces.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IItemService : IService<Item, ItemSearch>
    {
        Task<BaseListResponse<ItemDTOs>> GetItemsBySubCategoryIdAsync(int Id, int Gender, ItemSearch search);
        List<Item> GetItemsListBySubCategoryIdAsync(int Id, int Gender);
        Task<BaseListResponse<ItemDTOs>> GetItemsListAsync(ItemSearch search);
        Task<BaseListResponse<ItemDTO>> GetFilteredItemsAsync(ItemSearch search);
        Task<List<ItemDTO>> GetItemsWithImagesAsync(int subCategoryId);
        Task<List<ItemDTO>> GetItemsBySetIdAsync(int setId);
        Task<Item> GetItemByIdWithRelatedAsync(int itemId);
        Task<ItemDTOs> GetByIdAsync(int Id);
        bool Delete(int itemId);
        bool ToggleAllItems(int from, int to);

       Task<bool> DeleteSetItemAsync(int itemId);
        Task<bool> UpdateItemsCountAsync(int setId, int count);
    }
}
