namespace ORM.Repositories.Interfaces
{
    using Domain;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public interface ITourRepostirory
    {
        IQueryable<Tour> Filter(Expression<Func<Tour, bool>> filter);
        Tour GetById(int id);
        IQueryable<Tour> GetAll();
        bool TryGet(int id, out Tour tour);
    }
}
