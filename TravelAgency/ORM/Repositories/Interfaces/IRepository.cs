using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Repositories.Interfaces
{
    internal interface IRepository<TEntity>
        where TEntity: class 
    {
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        bool TryGet(int id, out TEntity entity);
        TEntity Create(TEntity entity);
        void Delete(int id);
    }
}
