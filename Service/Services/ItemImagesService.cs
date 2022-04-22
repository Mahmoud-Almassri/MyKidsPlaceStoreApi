using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.Context;
using Repository.Interfaces.Common;
using Repository.UnitOfWork;
using Service.Interfaces;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ItemImageservice : IItemImagesService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private MyKidsStoreDbContext _context;

        public ItemImageservice(IRepositoryUnitOfWork repositoryUnitOfWork, MyKidsStoreDbContext context)
        {
            _context = context;
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public async Task<ItemImages> AddAsync(ItemImages entity)
        {
            await _context.ItemImages.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }


        public async Task<ItemImages> UpdateAsync(ItemImages entity)
        {
            
            return await _repositoryUnitOfWork.ItemImages.Value.UpdateAsync(entity);
        }

        public async Task<ItemImages> GetAsync(int Id)
        {
            ItemImages ItemImages = await _repositoryUnitOfWork.ItemImages.Value.GetAsync(Id);
            return ItemImages;
        }

        public async Task<BaseListResponse<ItemImages>> ListAsync(BaseSearch search)
        {
            BaseListResponse<ItemImages> ItemImages = await _repositoryUnitOfWork.ItemImages.Value.List(x => true, search.PageSize, search.PageNumber);
            return ItemImages;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.ItemImages.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<ItemImages> AddRange(IEnumerable<ItemImages> entities)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemImages> GetAll()
        {
            return _repositoryUnitOfWork.ItemImages.Value.GetAll();
        }





        public IEnumerable<ItemImages> RemoveRange(IEnumerable<ItemImages> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemImages> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ItemImages> UpdateRange(IEnumerable<ItemImages> entities)
        {
            throw new NotImplementedException();
        }


    }
}

