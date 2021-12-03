
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class CountryRepository:IRepository<Country>
    {
        private TourContext _context;
        public CountryRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Country Create(Country country)
        {
            this._context.Country.Add(country);
            this._context.SaveChanges();
            return country;
        }
        public void Update(Country country)
        {
            _context.Set<Country>().Update(country);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            if (!this.TryGet(id, out var country))
            {
                return;
            }
            this._context.Country.Remove(country);
            this._context.SaveChanges();
        }

        public IQueryable<Country> Filter(Expression<Func<Country, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Country> GetAll() => _context.Country;

        public Country GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Country country)
        {
            country = this.GetAll().SingleOrDefault(t => t.Id == id);
            return country != null;
        }
    }
}
