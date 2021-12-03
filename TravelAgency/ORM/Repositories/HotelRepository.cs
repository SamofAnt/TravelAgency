namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    public class HotelRepository : IRepository<Hotel>
    {
        private TourContext _context;
        public HotelRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Hotel Create(Hotel hotel)
        {
            this._context.Hotel.Add(hotel);
            this._context.SaveChanges();
            return hotel;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var hotel))
            {
                return;
            }
            this._context.Hotel.Remove(hotel);
            this._context.SaveChanges();
        }

        public IQueryable<Hotel> Filter(Expression<Func<Hotel, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Hotel> GetAll() => _context.Hotel;

        public Hotel GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Hotel hotel)
        {
            hotel = this.GetAll().SingleOrDefault(t => t.Id == id);
            return hotel != null;
        }


       public void Update(Hotel hotel)
        {
            _context.Set<Hotel>().Update(hotel);
            _context.SaveChanges();
        }
    }
}
