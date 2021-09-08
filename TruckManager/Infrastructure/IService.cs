using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruckManager.Infrastructure
{
    public interface IService<TEntity>
        where TEntity : class, IEntity
    {
        public Task<List<TEntity>> GetAll();

        public Task<TEntity> Get(Guid id);

        public Task<ServiceResult<TEntity>> Add(TEntity entity);

        public Task<ServiceResult<TEntity>> Update(TEntity entity);

        public Task<TEntity> Delete(Guid id);
    }

    public class ServiceResult<TEntity>
        where TEntity : class, IEntity
    {
        public TEntity Entity { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; }
    }
}