
namespace ORM.Repositories
{
    using Domain;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    class TourRepository : ITourRepository
    {
        private readonly TourContext _context;
        public TourRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IQueryable<Tour> Filter(Expression<Func<Tour, bool>> filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));
            return this.GetAll().Where(filter);
        }

        public IQueryable<Tour> GetAll()
        {
            return this._context.Tour;
        }

        public Tour GetById(int id) => this.GetAll().SingleOrDefault(t => t.Id == id);

        public bool TryGet(int id, out Tour tour)
        {
            return (tour = this.GetById(id)) != null;
        }

    }
}
