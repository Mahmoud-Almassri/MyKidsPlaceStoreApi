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
    public class SetService : ISetService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public SetService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<Set> AddAsync(Set entity)
        {
            entity.Status = (int)GlobalStatusEnum.Active;

            return await _repositoryUnitOfWork.Set.Value.AddAsync(entity);
        }


        public async Task<Set> UpdateAsync(Set entity)
        {
            
            return await _repositoryUnitOfWork.Set.Value.UpdateAsync(entity);
        }

        public async Task<Set> GetAsync(int Id)
        {
            Set Set = await _repositoryUnitOfWork.Set.Value.GetAsync(Id);
            return Set;
        }

        public async Task<BaseListResponse<Set>> ListAsync(BaseSearch search)
        {
            BaseListResponse<Set> Sets = await _repositoryUnitOfWork.Set.Value.List(x=> true, search.PageSize, search.PageNumber);
            return Sets;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.Set.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<Set> AddRange(IEnumerable<Set> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Set> UpdateRange(IEnumerable<Set> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Set> RemoveRange(IEnumerable<Set> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Set> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Set> GetAll()
        {
            return _repositoryUnitOfWork.Set.Value.GetAll();
        }
    }
}
