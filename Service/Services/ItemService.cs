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
    public class ItemService : IItemService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private MyKidsStoreDbContext _context;

        public ItemService(IRepositoryUnitOfWork repositoryUnitOfWork, MyKidsStoreDbContext context)
        {
            _context = context;
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public async Task<Item> AddAsync(Item entity)
        {
            if (entity.IsSet)
            {
                if(!entity.SetId.HasValue)
                {
                    Set setObj = new Set
                    {
                        Status = (int)GlobalStatusEnum.Active,
                        SetName = " "
                    };
                    await _context.Set.AddAsync(setObj);
                    await _context.SaveChangesAsync();
                    
                    foreach(int itemId in entity.SetDTO.SetItems)
                    {
                       Item setItem = _context.Item.FirstOrDefault(x=>x.Id == itemId);
                        setItem.SetId = setObj.Id;
                        _context.Entry(setItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //_repositoryUnitOfWork.Item.Value.Update(setItem);
                        _context.SaveChanges();
                    }
                    entity.SetId = setObj.Id;
                }
            }
            await _context.Item.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        
        public async Task<Item> UpdateAsync(Item entity)
        {
            if (entity.IsSet)
            {
                if (!entity.SetId.HasValue)
                {
                    Set setObj = await _repositoryUnitOfWork.Set.Value.AddAsync(new Set
                    {
                        Status = (int)GlobalStatusEnum.Active,
                        SetName = ""
                    });

                    foreach (int itemId in entity.SetDTO.SetItems.Where(x => x != entity.Id))
                    {
                        Item setItem = await _repositoryUnitOfWork.Item.Value.GetAsync(itemId);
                        setItem.SetId = setObj.Id;
                        await _repositoryUnitOfWork.Item.Value.UpdateAsync(setItem);
                    }
                    entity.SetId = setObj.Id;
                }
                if(entity.SetId.HasValue && entity.SetDTO.SetItems.Any())
                {
                    foreach (int itemId in entity.SetDTO.SetItems.Where(x => x != entity.Id))
                    {
                        Item setItem = await _repositoryUnitOfWork.Item.Value.GetAsync(itemId);
                        setItem.SetId = entity.SetId;
                        await _repositoryUnitOfWork.Item.Value.UpdateAsync(setItem);
                    }
                }
            }
            await _repositoryUnitOfWork.Item.Value.UpdateAsync(entity);
            return entity;
        } 
        public async Task<bool> DeleteSetItemAsync(int itemId)
        {
            Item item = await _repositoryUnitOfWork.Item.Value.GetAsync(itemId);
            item.SetId = null;
            await _repositoryUnitOfWork.Item.Value.UpdateAsync(item);
            return true;
        }

        public async Task<ItemDTOs> GetByIdAsync(int Id)
        {
            Item item = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.Id == Id, 
                i => i.ItemImages, 
                i => i.Brand, 
                i => i.SubCategory, 
                i => i.Set,
                i => i.Sale);

            ItemDTOs itemDto = new ItemDTOs
            {
                Id = item.Id, 
                IsSet = item.IsSet,
                Status = item.Status,
                Price = item.Price,
                BrandId = item.BrandId, 
                Brand = new BrandDTO { 
                    Id = item.Brand.Id,
                    BrandName = item.Brand.BrandName,
                    Status = item.Brand.Status
                },
                ItemImages = item.ItemImages.Select(I => new ItemImagesDto
                {
                    Id = I.Id,
                    ImagePath = I.ImagePath,
                    Status = I.Status,
                    IsDefault = I.IsDefault,
                    ItemId = I.ItemId,
                }).ToList(),
                Sale = item.SaleId.HasValue ?
                   item.Sale.Status == (int)GlobalStatusEnum.Active ?
                   new SaleDTO
                   {
                       Id = item.Sale.Id,
                       CreatedDate = item.Sale.CreatedDate,
                       EndDate = item.Sale.EndDate,
                       NewPrice = item.Sale.NewPrice,
                       OldPrice = item.Sale.OldPrice,
                       Status = item.Sale.Status
                   }
                   : null
                   : null,
                Gender = item.Gender,
                ItemCount = item.ItemCount,
                ItemOrder = item.ItemOrder,
                SaleId = item.SaleId, 
                SetId = item.SetId,
                SubCategoryId = item.SubCategoryId,
                Title = item.Title,
                SubCategory = new SubCategoryDTO
                {
                        CategoryId = item.SubCategory.CategoryId,
                        CreatedDate = item.SubCategory.CreatedDate,
                        Description = item.SubCategory.Description,
                        Id = item.SubCategory.Id,
                        Name = item.SubCategory.Name,
                        NameAr = item.SubCategory.NameAr,
                        Status = item.SubCategory.Status 
                }
            };
            return itemDto;
        }
        public async Task<Item> GetItemByIdWithRelatedAsync(int itemId)
        {
            return await _repositoryUnitOfWork.Item.Value.GetItemByIdWithRelatedAsync(itemId);

        }
         public async Task<bool> UpdateItemsCountAsync(int setId, int count)
        {
            bool Result = await _repositoryUnitOfWork.Item.Value.UpdateItemsCountAsync(setId,count);
            return Result;

        }

     
        public async Task<BaseListResponse<ItemDTOs>> GetItemsListAsync(ItemSearch search)
        {
            BaseListResponse<ItemDTOs> Items = await _repositoryUnitOfWork.Item.Value.GetItemsListAsync(search);
            return Items;
        }
        public async Task<BaseListResponse<ItemDTO>> GetFilteredItemsAsync(ItemSearch search)
        {
            BaseListResponse<ItemDTO> Items = await _repositoryUnitOfWork.Item.Value.GetFilteredItemsAsync(search);
            return Items;
        }

        public async Task<BaseListResponse<ItemDTOs>> GetItemsBySubCategoryIdAsync(int Id, int Gender, ItemSearch search)
        {
            BaseListResponse<ItemDTOs> Items = await _repositoryUnitOfWork.Item.Value.GetItemBySubCategoryWithSortAsync( Id,  Gender,  search);
            return Items;
        }

        public List<Item> GetItemsListBySubCategoryIdAsync(int Id, int Gender)
        {
            List<Item> Items =  _repositoryUnitOfWork.Item.Value.Find(x => x.SubCategoryId == Id && (Gender == 4 || (x.Gender == Gender || x.Gender == (int) GenderEnum.All))  && x.Status != (int)GlobalStatusEnum.Empty && x.Status != (int)GlobalStatusEnum.InActive).ToList();
            return Items;
        }
        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Item.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<Item> AddRange(IEnumerable<Item> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> UpdateRange(IEnumerable<Item> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> RemoveRange(IEnumerable<Item> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAll()
        {
            return _repositoryUnitOfWork.Item.Value.GetAll();

        }
        public async Task<List<ItemDTO>> GetItemsWithImagesAsync(int subCategoryId)
        {
            return await _repositoryUnitOfWork.Item.Value.GetItemsWithImagesAsync(subCategoryId);
        } 
        public async Task<List<ItemDTO>> GetItemsBySetIdAsync(int setId)
        {
            return await _repositoryUnitOfWork.Item.Value.GetItemsBySetIdAsync(setId);
        }

        //public List<ItemDTO> GetSetItemsBySetId(int itemId)
        //{
        //    List<ItemDTO> items = _repositoryUnitOfWork.Item.Value.Find(x => x.SetId == itemId/* && x.IsSet == false */, x=>x.ItemImages)
        //        .Select(i=>new ItemDTO {
        //            BrandId = i.BrandId,
        //            BrandName = i.Brand.BrandName,
        //            DefaultImageUrl = i.ItemImages.Where(x => x.IsDefault).FirstOrDefault().ImagePath,
        //            Id = i.Id,
        //            IsSet = i.IsSet,
        //            ItemCount = i.ItemCount,
        //            Price = i.Price,
        //            SubCategoryId = i.SubCategoryId,
        //            SubCategoryName = i.SubCategory.Name,
        //            CategoryName = i.SubCategory.Category.CategoryName,
        //            Status = i.Status,
        //        }).ToList();
        //    return items;
        //}

        public bool Delete(int itemId)
        {
            return _repositoryUnitOfWork.Item.Value.Delete(itemId);
        }

        public bool ToggleAllItems(int from, int to)
        {
            List<Item> items = _repositoryUnitOfWork.Item.Value.Find(x => x.Status == from).ToList();

            foreach(Item item in items)
            {
                item.Status = to;
                
            }
            _repositoryUnitOfWork.Item.Value.UpdateRangeAsync(items);
            return true;
        }

        public async Task<Item> GetAsync(int Id)
        {
            Item item = await _repositoryUnitOfWork.Item.Value.FirstOrDefault(x => x.Id == Id,
            i => i.ItemImages,
            i => i.Brand,
            i => i.SubCategory,
            i => i.Set,
            i => i.Sale);
            return item;
        }

        public Task<BaseListResponse<Item>> ListAsync(ItemSearch entity)
        {
            throw new NotImplementedException();
        }
    }
}
