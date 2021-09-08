using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public IQueryable<TEntity> GetQueryable();

        public Task<List<TEntity>> GetAll();

        public Task<TEntity> Get(Guid id);

        public Task<TEntity> Add(TEntity entity);

        public Task<TEntity> Update(TEntity entity);

        public Task<TEntity> Delete(TEntity entity);
    }
}