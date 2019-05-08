using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BomberMan.Data.Entities;

namespace BomberMan.Repository.Feature.Repository.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(Guid id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);

        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
