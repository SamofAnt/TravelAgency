
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    class TourRepository : IRepository<Tour>
    {
        private readonly TourContext _context;
        public TourRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Tour Create(Tour tour)
        {
            this._context.Tour.Add(tour);
            this._context.SaveChanges();
            return tour;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var tour))
            {
                return;
            }
            this._context.Tour.Remove(tour);
            this._context.SaveChanges();
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

        public Tour Update(Tour entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Tour>.Update(Tour entity)
        {
            throw new NotImplementedException();
        }
    }
}
