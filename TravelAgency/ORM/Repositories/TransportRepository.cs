
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    class TransportRepository : IRepository<Transport>
    {
        private TourContext _context;
        public TransportRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Transport Create(Transport transport)
        {
            this._context.Transport.Add(transport);
            this._context.SaveChanges();
            return transport;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var transport))
            {
                return;
            }
            this._context.Transport.Remove(transport);
            this._context.SaveChanges();
        }

        public IQueryable<Transport> Filter(Expression<Func<Transport, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Transport> GetAll() => _context.Transport;

        public Transport GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Transport transport)
        {
            transport = this.GetAll().SingleOrDefault(t => t.Id == id);
            return transport != null;
        }

        public Transport Update(Transport entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Transport>.Update(Transport entity)
        {
            throw new NotImplementedException();
        }
    }
}
