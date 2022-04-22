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
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public CategoryService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<Category> AddAsync(Category entity)
        {
            entity.Status = (int) GlobalStatusEnum.Active;
            await _repositoryUnitOfWork.Category.Value.AddAsync(entity);
            return entity;
        }


        public async Task<Category> UpdateAsync(Category entity)
        {
            
            
            return await _repositoryUnitOfWork.Category.Value.UpdateAsync(entity);
        }

        public async Task<Category> GetAsync(int Id)
        {
            Category Category = await _repositoryUnitOfWork.Category.Value.GetAsync(Id);
            return Category;
        }

        public async Task<BaseListResponse<Category>> ListAsync(BaseSearch search)
        {
            return await _repositoryUnitOfWork.Category.Value.List(x => string.IsNullOrEmpty(search.Name) || (x.CategoryName.Contains(search.Name) || x.CategoryNameAr.Contains(search.Name)), search.PageSize, search.PageNumber);
        }

        //public List<Category> GetCategoryByMasterCategoryId(int Id, BaseSearch search)
        //{
        //    List<Category> Categorys = _repositoryUnitOfWork.Category.Value.List(x => x.MasterCategoryId == Id && string.IsNullOrEmpty(search.Name) || x.CategoryName.Contains(search.Name), search.PageSize, search.PageNumber);
        //    return Categorys;
        //}

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Category.Value.RemoveAsync(Id);
            return true;
        }
    
        public IEnumerable<Category> AddRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> UpdateRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return _repositoryUnitOfWork.Category.Value.Find(x=>x.Status != (int) GlobalStatusEnum.InActive);
        }

        
    }
}
