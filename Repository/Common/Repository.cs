using Domains.Common;
using Domains.DTO;
using Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Context;
using Repository.Interfaces;
using Repository.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Common
{
    public class Repository<IEntity> : IRepository<IEntity> where IEntity : BaseModel, new()
    {
        #region [Context]
        protected MyKidsStoreDbContext Context;
        #endregion

        public Repository(MyKidsStoreDbContext context)
        {
            Context = context;
        }

        #region Get
        public async Task<IEntity> GetAsync(int Id)
        {
            IEntity result = await Context.Set<IEntity>().FirstOrDefaultAsync(entity => entity.Id == Id);
            return result;
        }

        #endregion

        #region GetAll
        public IEnumerable<IEntity> GetAll()
        {
                IQueryable<IEntity> dbQuery = Context.Set<IEntity>();
                return dbQuery.AsNoTracking();         
        }

        #endregion

        #region FirstOrDefault
        public async Task<IEntity> FirstOrDefault(Expression<Func<IEntity, bool>> where)
          {
            IEntity result = await Context.Set<IEntity>().AsNoTracking().FirstOrDefaultAsync(where);
            return result;
          }
        #endregion

        #region Any
        public bool Any(Expression<Func<IEntity, bool>> where)
        {
            bool result = Context.Set<IEntity>().Any(where);
            return result;
        }
        #endregion

        #region FirstOrDefault
        public async Task<IEntity> FirstOrDefault(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>().AsNoTracking();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<IEntity, object>(navigationProperty);
            }

            return await dbQuery.Where(where).FirstOrDefaultAsync();
        }
        #endregion

        #region Find
        public IEnumerable<IEntity> Find(Expression<Func<IEntity, bool>> predicate, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            return dbQuery.Where(predicate);
        }
        #endregion

        #region List
        public async Task<BaseListResponse<IEntity>> List(Expression<Func<IEntity, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();
            int totalCount = dbQuery.Count();
            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }


            return new BaseListResponse<IEntity>
            {
                TotalCount = totalCount,
                entities = await dbQuery.Where(predicate)
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize)
                .OrderByDescending(x=>x.Id)
                .ToListAsync()
            };
        }
        #endregion

        #region Add
        public async Task<IEntity> AddAsync(IEntity entity)
        {
            Context.Set<IEntity>().Add(entity);
            await SaveChanges();
            Context.Entry(entity).GetDatabaseValues();
            return entity;
        }
        #endregion

        #region AddRnage
        public async Task<IEnumerable<IEntity>> AddRangeAsync(IEnumerable<IEntity> entities)
        {
            Context.ChangeTracker.Entries<IEntity>();
            Context.Set<IEntity>().AddRange(entities);
            await SaveChanges();
            return entities;
        }
        #endregion

        #region Update
        public async Task<IEntity> UpdateAsync(IEntity entity, bool disableAttach = false)
        {
            Context.Set<IEntity>().Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
            return entity;
        }
        #endregion

        #region UpdateRange
        public async Task<IEnumerable<IEntity>> UpdateRangeAsync(IEnumerable<IEntity> Entities)
        {
            Context.Set<IEntity>().UpdateRange(Entities);
            await SaveChanges();
            return Entities;
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveAsync(int Id)
        {
            IEntity entity = await GetAsync(Id);
            if(entity != null)
            {
                Context.Set<IEntity>().Remove(entity);
                await SaveChanges();
            }
     
            return true;
        }
        #endregion

        #region RemoveRange
        public async Task<IEnumerable<IEntity>> RemoveRangeAsync(IEnumerable<IEntity> entities)
        {
            Context.Set<IEntity>().RemoveRange(entities);
            await SaveChanges();
            return entities;
        }
        #endregion

        #region SaveChanges
        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
        #endregion

        #region BeginTransaction
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }
        #endregion

        #region CommitTransaction
        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }
        #endregion

        #region RollbackTransaction
        public void RollbackTransaction()
        {
            Context.Database.RollbackTransaction();
        }
        #endregion

    }
}
