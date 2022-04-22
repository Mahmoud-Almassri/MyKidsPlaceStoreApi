using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ItemsRepository : Repository<Item>, IItemRepository
    {
        private MyKidsStoreDbContext _context;
        public ItemsRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Item> GetItemByIdWithRelatedAsync(int itemId)
        {
            return await _context.Item.Where(x => x.Id == itemId)
                                .Include(x => x.ItemImages)
                                .Include(x => x.Sale)
                                .Include(x => x.Brand)
                                .Include(x => x.SubCategory)
                                .Include(x => x.Set)
                                .FirstOrDefaultAsync();
        }

        public async Task<BaseListResponse<ItemDTOs>> GetItemBySubCategoryWithSortAsync(int Id, int Gender, ItemSearch search)
        {
            List<ItemDTOs> dbQuery = await _context.Item.Where(x => x.SubCategoryId == Id && (Gender == 4 || (x.Gender == Gender || x.Gender == (int)GenderEnum.All)) && x.Status != (int)GlobalStatusEnum.Empty && x.Status != (int)GlobalStatusEnum.InActive)
                .OrderByDescending(u => u.ItemOrder.HasValue)
                .ThenBy(u => u.ItemOrder)
                .Skip(search.PageSize * (search.PageNumber - 1))
                .Take(search.PageSize)
                .Include(x => x.Sale)
                .Include(x => x.ItemImages)
               .Select(x => new ItemDTOs
               {

                   BrandId = x.BrandId,
                   Gender = x.Gender,
                   ItemCount = x.ItemCount,
                   Id = x.Id,
                   IsSet = x.IsSet,
                   ItemImages = x.ItemImages.Select(I => new ItemImagesDto
                   {
                       Id = I.Id,
                       ImagePath = I.ImagePath,
                       Status = I.Status,
                       IsDefault = I.IsDefault,
                       ItemId = I.ItemId,
                   }).ToList(),
                   SaleId = x.SaleId,
                   Status = x.Status,
                   Title = x.Title,
                   SubCategoryId = x.SubCategoryId,
                   Price = x.Price,
                   SetId = x.SetId,
                   ItemOrder = x.ItemOrder,
                   Sale = x.SaleId.HasValue ? 
                   x.Sale.Status == (int) GlobalStatusEnum.Active ? 
                   new SaleDTO
                   {
                       Id = x.Sale.Id,
                       CreatedDate = x.Sale.CreatedDate,
                       EndDate = x.Sale.EndDate,
                       NewPrice = x.Sale.NewPrice,
                       OldPrice = x.Sale.OldPrice,
                       Status = x.Sale.Status
                   } 
                   : null
                   : null,
               })
                .ToListAsync();
            int totalCount = _context.Item.Where(x => x.SubCategoryId == Id && (Gender == 4 || (x.Gender == Gender || x.Gender == (int)GenderEnum.All)) && x.Status != (int)GlobalStatusEnum.Empty && x.Status != (int)GlobalStatusEnum.InActive).Count();



            return new BaseListResponse<ItemDTOs>
            {
                TotalCount = totalCount,
                entities = dbQuery
            };
        }

         public async Task<BaseListResponse<ItemDTOs>> GetItemsListAsync(ItemSearch search)
        {
            List<ItemDTOs> dbQuery = await _context.Item
                .Where(x =>
            x.Status != (int)GlobalStatusEnum.Empty && x.Status != (int)GlobalStatusEnum.InActive
         && (string.IsNullOrEmpty(search.Brand) || x.Brand.BrandName.Contains(search.Brand))
         && (string.IsNullOrEmpty(search.Title) || x.Title.Contains(search.Title))
         && (!search.MinPrice.HasValue || x.Price >= search.MinPrice)
         && (!search.MaxPrice.HasValue || x.Price <= search.MaxPrice)
         && (!search.Gender.HasValue || x.Gender == search.Gender)
         && (string.IsNullOrEmpty(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName))))
                .Skip(search.PageSize * (search.PageNumber - 1))
                .Take(search.PageSize)
                .Include(x => x.Sale)
                .Include(x => x.ItemImages)
               .Select(x => new ItemDTOs
               {

                   BrandId = x.BrandId,
                   Gender = x.Gender,
                   ItemCount = x.ItemCount,
                   Id = x.Id,
                   IsSet = x.IsSet,
                   ItemImages = x.ItemImages.Select(I => new ItemImagesDto
                   {
                       Id = I.Id,
                       ImagePath = I.ImagePath,
                       Status = I.Status,
                       IsDefault = I.IsDefault,
                       ItemId = I.ItemId,
                   }).ToList(),
                   SaleId = x.SaleId,
                   Status = x.Status,
                   Title = x.Title,
                   SubCategoryId = x.SubCategoryId,
                   Price = x.Price,
                   SetId = x.SetId,
                   ItemOrder = x.ItemOrder,
                   Sale = x.SaleId.HasValue ? new SaleDTO
                   {
                       Id = x.Sale.Id,
                       CreatedDate = x.Sale.CreatedDate,
                       EndDate = x.Sale.EndDate,
                       NewPrice = x.Sale.NewPrice,
                       OldPrice = x.Sale.OldPrice,
                       Status = x.Sale.Status
                   } : null,
               })
                .ToListAsync();
            int totalCount = await _context.Item
                .Where(x =>
            x.Status != (int)GlobalStatusEnum.Empty && x.Status != (int)GlobalStatusEnum.InActive
         && (string.IsNullOrEmpty(search.Brand) || x.Brand.BrandName.Contains(search.Brand))
         && (string.IsNullOrEmpty(search.Title) || x.Title.Contains(search.Title))
         && (!search.MinPrice.HasValue || x.Price >= search.MinPrice)
         && (!search.MaxPrice.HasValue || x.Price <= search.MaxPrice)
         && (!search.Gender.HasValue || x.Gender == search.Gender)
         && (string.IsNullOrEmpty(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName))))
                .CountAsync();



            return new BaseListResponse<ItemDTOs>
            {
                TotalCount = totalCount,
                entities = dbQuery
            };
        }

        public async Task<BaseListResponse<ItemDTO>> GetFilteredItemsAsync(ItemSearch search)
        {
            int ItemsCount = await _context.Item
                .Where(x =>
             (string.IsNullOrEmpty(search.Title) || x.Title.Contains(search.Title)) &&
             (string.IsNullOrEmpty(search.Brand) || x.Brand.BrandName.Contains(search.Brand)) &&
             (!search.MinPrice.HasValue || x.Price >= search.MinPrice) &&
             (!search.SetId.HasValue || x.SetId == search.SetId) &&
             (!search.MaxPrice.HasValue || x.Price <= search.MaxPrice) &&
             (!search.Gender.HasValue || x.Gender == search.Gender) &&
             (!search.Status.HasValue || x.Status == search.Status) &&
             (!search.ItemId.HasValue || x.Id == search.ItemId) &&
             (string.IsNullOrEmpty(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName)))))
                .CountAsync();
            List<ItemDTO> Items = await _context.Item.Where(x =>
             (string.IsNullOrEmpty(search.Title) || x.Title.Contains(search.Title)) &&
             (string.IsNullOrEmpty(search.Brand) || x.Brand.BrandName.Contains(search.Brand)) &&
             (!search.MinPrice.HasValue || x.Price >= search.MinPrice) &&
             (!search.SetId.HasValue || x.SetId == search.SetId) &&
             (!search.MaxPrice.HasValue || x.Price <= search.MaxPrice) &&
             (!search.Gender.HasValue || x.Gender == search.Gender) &&
             (!search.Status.HasValue || x.Status == search.Status) &&
             (!search.ItemId.HasValue || x.Id == search.ItemId) &&
             (string.IsNullOrEmpty(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName) || (x.SubCategory.Name.Contains(search.SubCategoryName)))))
                                .Include(x => x.ItemImages)
                                .Include(x => x.Brand)
                                .Include(x => x.SubCategory)
                                .ThenInclude(x => x.Category)
                                .Skip(search.PageSize * (search.PageNumber - 1))
                                .Take(search.PageSize)
                                .Select(i => new ItemDTO
                                {
                                    Title = i.Title,
                                    BrandId = i.BrandId,
                                    BrandName = i.Brand.BrandName,
                                    DefaultImageUrl = i.ItemImages.Where(x => x.IsDefault).FirstOrDefault().ImagePath,
                                    Id = i.Id,
                                    IsSet = i.IsSet,
                                    ItemCount = i.ItemCount,
                                    Price = i.Price,
                                    SubCategoryId = i.SubCategoryId,
                                    SubCategoryName = i.SubCategory.Name,
                                    CategoryName = i.SubCategory.Category.CategoryName,
                                    Status = i.Status,
                                })
                                .OrderByDescending(x => x.Id)
                                .ToListAsync();
            return new BaseListResponse<ItemDTO>
            {
                TotalCount = ItemsCount,
                entities = Items
            };
        }
        public async Task<bool> UpdateItemsCountAsync(int setId, int count)
        {
            List<Item> items = await _context.Item.Where(x => x.SetId == setId && x.IsSet == false).ToListAsync();
            foreach (var item in items)
            {
                item.ItemCount = item.ItemCount - count;
                if (item.ItemCount <= 0)
                {
                    item.Status = GlobalStatusEnum.Empty.GetHashCode();
                }
                await UpdateItemAsync(item);
            }
            
            return true;

        }
        public async Task<List<ItemDTO>> GetItemsWithImagesAsync(int subCategoryId)
        {
            int ItemsCount = await _context.Item
                 .Where(x => x.SubCategoryId == subCategoryId)
                .CountAsync();
            List<ItemDTO> Items =await _context.Item
                .Where(x => x.SubCategoryId == subCategoryId)
                .Include(x => x.ItemImages)
                                .Select(i => new ItemDTO
                                {
                                    Title = i.Title,
                                    BrandId = i.BrandId,
                                    BrandName = i.Brand.BrandName,
                                    DefaultImageUrl = i.ItemImages.Where(x => x.IsDefault).FirstOrDefault().ImagePath,
                                    Id = i.Id,
                                    IsSet = i.IsSet,
                                    ItemCount = i.ItemCount,
                                    Price = i.Price,
                                    SubCategoryId = i.SubCategoryId,
                                    SubCategoryName = i.SubCategory.Name,
                                    CategoryName = i.SubCategory.Category.CategoryName,
                                    Status = i.Status,
                                })
                                .OrderByDescending(x => x.Id)
                                .ToListAsync();
            return Items;
        }
        public async Task<List<ItemDTO>> GetItemsBySetIdAsync(int setId)
        {
            int ItemsCount = await _context.Item
                .Where(x => x.SetId == setId && x.IsSet == false)
                .CountAsync();
            List<ItemDTO> Items = await _context.Item
                .Where(x => x.SetId == setId && x.IsSet == false)
                .Include(x => x.ItemImages)
                                .Select(i => new ItemDTO
                                {
                                    Title = i.Title,
                                    BrandId = i.BrandId,
                                    BrandName = i.Brand.BrandName,
                                    DefaultImageUrl = i.ItemImages.Where(x => x.IsDefault).FirstOrDefault().ImagePath,
                                    Id = i.Id,
                                    IsSet = i.IsSet,
                                    ItemCount = i.ItemCount,
                                    Price = i.Price,
                                    SubCategoryId = i.SubCategoryId,
                                    SubCategoryName = i.SubCategory.Name,
                                    CategoryName = i.SubCategory.Category.CategoryName,
                                    Status = i.Status,
                                })
                                .OrderByDescending(x => x.Id)
                                .ToListAsync();
            return Items;
        }
        public bool Delete(int itemId)
        {
            Item item = _context.Item.Where(x => x.Id == itemId).Include(x => x.CartItem).Include(x => x.ItemImages).FirstOrDefault();
            if (item.CartItem.Count > 0)
            {
                return false;
            }
            if (item.ItemImages.Count > 0)
            {
                _context.RemoveRange(item.ItemImages);
                _context.SaveChanges();
            }
            _context.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(item).State = EntityState.Detached;
            await  _context.SaveChangesAsync();
            return true;
        }
    }
}
