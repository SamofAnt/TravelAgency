
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    class TouristRepository : IRepository<Tourist>
    {
        private readonly TourContext _context;
        public TouristRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Tourist Create(Tourist tourist)
        {
            this._context.Tourist.Add(tourist);
            this._context.SaveChanges();
            return tourist;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var tourist))
            {
                return;
            }
            this._context.Tourist.Remove(tourist);
            this._context.SaveChanges();
        }

        public IQueryable<Tourist> Filter(Expression<Func<Tourist, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Tourist> GetAll() => _context.Tourist;

        public Tourist GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Tourist tourist)
        {
            tourist = this.GetAll().SingleOrDefault(t => t.Id == id);
            return tourist != null;
        }

        public Tourist Update(Tourist entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Tourist>.Update(Tourist entity)
        {
            throw new NotImplementedException();
        }
    }
}
