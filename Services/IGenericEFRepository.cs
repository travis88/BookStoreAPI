using System.Collections.Generic;

namespace AspNetCorePublisherWebAPI.Services
{
    public interface IGenericEFRepository
    {
        IEnumerable<TEntity> Get<TEntity>() where TEntity : class;

        TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class;

        bool Save();

        void Add<TEntity>(TEntity item) where TEntity : class;

        bool Exists<TEntity>(int id) where TEntity : class;

        void Delete<TEntity>(TEntity item) where TEntity : class;
    }
}