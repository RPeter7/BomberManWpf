using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BomberMan.Data.Entities;
using BomberMan.Repository.Feature.Repository.Interfaces;

namespace BomberMan.Repository.Feature.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(Guid id) => _dbContext.Set<T>().Find(id);

        public IEnumerable<T> GetAll() => _dbContext.Set<T>();
        
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate) => _dbContext.Set<T>().Where(predicate);
        
        public void Insert(T entity) => _dbContext.Set<T>().Add(entity);
            
        public void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;
           
        public void Delete(T entity) =>  _dbContext.Set<T>().Remove(entity);
    }
}
