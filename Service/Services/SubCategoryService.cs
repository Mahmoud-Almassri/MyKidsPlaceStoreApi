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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public SubCategoryService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<SubCategory> AddAsync(SubCategory entity)
        {
            entity.CreatedDate = DateTime.Now;
            
            return await _repositoryUnitOfWork.SubCategory.Value.AddAsync(entity);
        }


        public async Task<SubCategory> UpdateAsync(SubCategory entity)
        {
            entity.ModifiedDate = DateTime.Now;
            
            return await _repositoryUnitOfWork.SubCategory.Value.UpdateAsync(entity);
        }

        public async Task<SubCategory> GetAsync(int Id)
        {
            SubCategory SubCategory = await _repositoryUnitOfWork.SubCategory.Value.GetAsync(Id);
            return SubCategory;
        }

        public async Task<BaseListResponse<SubCategory>> ListAsync(BaseSearch search)
        {
            BaseListResponse<SubCategory> SubCategorys = await _repositoryUnitOfWork.SubCategory.Value.List(x => string.IsNullOrEmpty(search.Name) || (x.Name.Contains(search.Name) || x.NameAr.Contains(search.Name)), search.PageSize, search.PageNumber);
            return SubCategorys;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.SubCategory.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<SubCategory> GetByParentId(int Id)
        {
            IEnumerable<SubCategory> SubCategories = _repositoryUnitOfWork.SubCategory.Value.Find(x=>x.CategoryId == Id && x.Status != (int) GlobalStatusEnum.InActive).OrderBy(x=>x.OrderNumber);
            return SubCategories;
        }
        public IEnumerable<SubCategory> AddRange(IEnumerable<SubCategory> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> UpdateRange(IEnumerable<SubCategory> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> RemoveRange(IEnumerable<SubCategory> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> GetAll()
        {
            return _repositoryUnitOfWork.SubCategory.Value.Find(x => x.Status != (int)GlobalStatusEnum.InActive).OrderBy(x => x.OrderNumber);
        }
    }
}
