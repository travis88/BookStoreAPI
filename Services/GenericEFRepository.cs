using AspNetCorePublisherWebAPI.Entities;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace AspNetCorePublisherWebAPI.Services
{
    public class GenericEFRepository : IGenericEFRepository
    {
        private SqlDbContext _db;

        public GenericEFRepository(SqlDbContext db)
        {
            _db = db;
        }

        public IEnumerable<TEntity> Get<TEntity>() where TEntity : class
        {
            return _db.Set<TEntity>();
        }

        public TEntity Get<TEntity>(int id, bool includeRelatedEntities = false) where TEntity : class
        {
            var entity = _db.Set<TEntity>().Find(new object[] { id });
            
            if (entity != null && includeRelatedEntities)
            {
                var dbsets = typeof(SqlDbContext)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(z => z.PropertyType.Name.Contains("DbSet"))
                    .Select(z => z.Name);

                var tables = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(z => dbsets.Contains(z.Name))
                    .Select(z => z.Name);

                if (tables.Count() > 0)
                {
                    foreach (var table in tables)
                    {
                        _db.Entry(entity).Collection(table).Load();
                    }
                }
            }

            return entity;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public void Add<TEntity>(TEntity item) where TEntity : class 
        {
            _db.Add<TEntity>(item);
        }

        public bool Exists<TEntity>(int id) where TEntity : class
        {
            return _db.Set<TEntity>().Find(new object[] { id }) != null;
        }

        public void Delete<TEntity>(TEntity item) where TEntity : class
        {
            _db.Set<TEntity>().Remove(item);
        }
    }
}