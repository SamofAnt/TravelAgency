

namespace ORM.Repositories
{
    using Domain;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ITouristRepository
    {
        IQueryable<Tourist> Filter(Expression<Func<Tourist, bool>> filter);
        Tourist GetById(int id);
        IQueryable<Tourist> GetAll();
        bool TryGet(int id, out Tourist tourist);
        Tourist Create(Tourist tourist);
        void Delete(int id);
    }
}
