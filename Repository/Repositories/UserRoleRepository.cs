using Domains.DTO;
using Domains.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Context;
using Repository.Interfaces;
using Repository.Interfaces.Common;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private MyKidsStoreDbContext _context;
        public UserRoleRepository(MyKidsStoreDbContext context)
        {
            _context = context;
        }

        public async Task<UserRoles> AddAsync(UserRoles entity)
        {
            await _context.UserRoles.AddAsync(entity);
            await  _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<UserRoles> AddRangeAsync(IEnumerable<UserRoles> entities)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<UserRoles, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> Find(Expression<Func<UserRoles, bool>> where, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public UserRoles FirstOrDefaultAsync(Expression<Func<UserRoles, bool>> where, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public UserRoles FirstOrDefault(Expression<Func<UserRoles, bool>> where)
        {
            UserRoles result = _context.UserRoles.FirstOrDefault(where);
            return result;
        }

        public UserRoles GetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserRoles> List(Expression<Func<UserRoles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> RemoveRangeAsync(IEnumerable<UserRoles> entities)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public UserRoles UpdateAsync(UserRoles entity, bool disableAttach = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> UpdateRangeAsync(IEnumerable<UserRoles> Entities)
        {
            throw new NotImplementedException();
        }

       

        Task<UserRoles> IRepository<UserRoles>.GetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserRoles>> IRepository<UserRoles>.AddRangeAsync(IEnumerable<UserRoles> entities)
        {
            throw new NotImplementedException();
        }

        Task<UserRoles> IRepository<UserRoles>.UpdateAsync(UserRoles entity, bool disableAttach)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserRoles>> IRepository<UserRoles>.UpdateRangeAsync(IEnumerable<UserRoles> Entities)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<UserRoles>.RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserRoles>> IRepository<UserRoles>.RemoveRangeAsync(IEnumerable<UserRoles> entities)
        {
            throw new NotImplementedException();
        }

        Task<BaseListResponse<UserRoles>> IRepository<UserRoles>.List(Expression<Func<UserRoles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<UserRoles> FirstOrDefault(Expression<Func<UserRoles, bool>> where, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        Task<UserRoles> IRepository<UserRoles>.FirstOrDefault(Expression<Func<UserRoles, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
