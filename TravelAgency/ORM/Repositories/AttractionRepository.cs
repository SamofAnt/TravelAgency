
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    class AttractionRepository:IRepository<Attraction>
    {
        private TourContext _context;
        public AttractionRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Attraction Create(Attraction attraction)
        {
            this._context.Attraction.Add(attraction);
            this._context.SaveChanges();
            return attraction;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var attraction))
            {
                return;
            }
            this._context.Attraction.Remove(attraction);
            this._context.SaveChanges();
        }

        public IQueryable<Attraction> Filter(Expression<Func<Attraction, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Attraction> GetAll() => _context.Attraction;

        public Attraction GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Attraction attraction)
        {
            attraction = this.GetAll().SingleOrDefault(t => t.Id == id);
            return attraction != null;
        }
    }
}
