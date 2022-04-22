using Domains.DTO;
using Domains.Models;
using Microsoft.EntityFrameworkCore;
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
    public class RoleRepository : IRoleRepository
    {
        private MyKidsStoreDbContext _context;
        public RoleRepository(MyKidsStoreDbContext context) 
        {
            _context = context;
        }

        public async Task<Roles> AddAsync(Roles entity)
        {
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<Roles> AddRangeAsync(IEnumerable<Roles> entities)
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

        public IEnumerable<Roles> Find(Expression<Func<Roles, bool>> where, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Roles> FirstOrDefault(Expression<Func<Roles, bool>> where, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            Roles result = await _context.Roles.FirstOrDefaultAsync(where);
            return result;
        }

        public async Task<Roles> FirstOrDefault(Expression<Func<Roles, bool>> where)
        {
            Roles result = await _context.Roles.FirstOrDefaultAsync(where);
            return result;
        }


        public bool Any(Expression<Func<Roles, bool>> where)
        {
            bool result = _context.Roles.Any(where);
            return result;

        }

        public Roles Get(long Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> GetAll()
        {
            throw new NotImplementedException();
        }

        public Roles Remove(Roles entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> RemoveRangeAsync(IEnumerable<Roles> entities)
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

        public Roles UpdateAsync(Roles entity, bool disableAttach = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> UpdateRangeAsync(IEnumerable<Roles> Entities)
        {
            throw new NotImplementedException();
        }

        public Roles GetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Roles> List(Expression<Func<Roles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        Task<Roles> IRepository<Roles>.GetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Roles>> IRepository<Roles>.AddRangeAsync(IEnumerable<Roles> entities)
        {
            throw new NotImplementedException();
        }

        Task<Roles> IRepository<Roles>.UpdateAsync(Roles entity, bool disableAttach)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Roles>> IRepository<Roles>.UpdateRangeAsync(IEnumerable<Roles> Entities)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<Roles>.RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Roles>> IRepository<Roles>.RemoveRangeAsync(IEnumerable<Roles> entities)
        {
            throw new NotImplementedException();
        }

        Task<BaseListResponse<Roles>> IRepository<Roles>.List(Expression<Func<Roles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

     

        

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
