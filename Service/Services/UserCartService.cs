using Domains.DTO;
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
    public class UserCartService : IUserCartService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public UserCartService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }
        public async Task<UserCart> AddAsync(UserCart entity)
        {
            return await _repositoryUnitOfWork.UserCart.Value.AddAsync(entity);
        }


        public async Task<UserCart> UpdateAsync(UserCart entity)
        {

            return await _repositoryUnitOfWork.UserCart.Value.UpdateAsync(entity);
        }

        public async Task<UserCart> GetAsync(int Id)
        {
            UserCart UserCart = await _repositoryUnitOfWork.UserCart.Value.GetAsync(Id);
            return UserCart;
        }

        public async Task<BaseListResponse<UserCart>> ListAsync(BaseSearch search)
        {
            BaseListResponse<UserCart> UserCarts = await _repositoryUnitOfWork.UserCart.Value.List(x => true, search.PageSize, search.PageNumber);
            return UserCarts;
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            await _repositoryUnitOfWork.UserCart.Value.RemoveAsync(Id);
            return true;
        }

        public IEnumerable<UserCart> AddRange(IEnumerable<UserCart> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCart> UpdateRange(IEnumerable<UserCart> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCart> RemoveRange(IEnumerable<UserCart> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCart> RemoveRangeByIDs(IEnumerable<long> IDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCart> GetAll()
        {
            return _repositoryUnitOfWork.UserCart.Value.GetAll();

        }
    }
}
