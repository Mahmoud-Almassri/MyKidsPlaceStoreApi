using Domains.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Common
{
    public interface IService<Entity, TEntity>
    {
        Task<Entity> AddAsync(Entity entity);
        IEnumerable<Entity> AddRange(IEnumerable<Entity> entities);
        Task<Entity>  UpdateAsync(Entity entity);
        IEnumerable<Entity> UpdateRange(IEnumerable<Entity> entities);
        Task<bool> RemoveAsync(int Id);
        IEnumerable<Entity> RemoveRange(IEnumerable<Entity> entities);
        IEnumerable<Entity> RemoveRangeByIDs(IEnumerable<long> IDs);
        Task<Entity> GetAsync(int Id);
        IEnumerable<Entity> GetAll();
        Task<BaseListResponse<Entity>> ListAsync(TEntity entity);

    }
}
