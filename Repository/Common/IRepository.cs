using Domains.DTO;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Common
{
    public interface IRepository<IEntity> where IEntity : class
    {
        Task<IEntity> GetAsync(int Id);
        IEnumerable<IEntity> GetAll();
        Task<IEntity> AddAsync(IEntity entity);
        Task<IEnumerable<IEntity>> AddRangeAsync(IEnumerable<IEntity> entities);
        Task<IEntity> UpdateAsync(IEntity entity, bool disableAttach = false);
        Task<IEnumerable<IEntity>> UpdateRangeAsync(IEnumerable<IEntity> Entities);
        Task<bool> RemoveAsync(int Id);
        Task<IEnumerable<IEntity>> RemoveRangeAsync(IEnumerable<IEntity> entities);
        Task<BaseListResponse<IEntity>> List(Expression<Func<IEntity, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<IEntity, object>>[] navigationProperties);
        IEnumerable<IEntity> Find(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties);
        Task<IEntity> FirstOrDefault(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties);
        Task<IEntity> FirstOrDefault(Expression<Func<IEntity, bool>> where);
        bool Any(Expression<Func<IEntity, bool>> where);
        Task SaveChanges();
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}