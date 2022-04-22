using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.UnitOfWork;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BrandService : IBrandService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public BrandService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<Brand> AddAsync(Brand entity)
        {
            await _repositoryUnitOfWork.Brand.Value.AddAsync(entity);
            return entity;
        }


        public async Task<Brand> UpdateAsync(Brand entity)
        {
            await _repositoryUnitOfWork.Brand.Value.UpdateAsync(entity);
            return entity;
        }

        public async Task<Brand> GetAsync(int Id)
        {
            return  await _repositoryUnitOfWork.Brand.Value.GetAsync(Id);
        }

        public async Task<BaseListResponse<Brand>> ListAsync(BaseSearch search)
        {
            return await _repositoryUnitOfWork.Brand.Value.List(x => string.IsNullOrEmpty(search.Name) || x.BrandName.Contains(search.Name), search.PageSize, search.PageNumber);
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Brand.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<Brand> AddRange(IEnumerable<Brand> entities)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Brand> GetAll()
        {
            return _repositoryUnitOfWork.Brand.Value.GetAll();
        }





        public IEnumerable<Brand> RemoveRange(IEnumerable<Brand> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Brand> UpdateRange(IEnumerable<Brand> entities)
        {
            throw new NotImplementedException();
        }


    }
}
