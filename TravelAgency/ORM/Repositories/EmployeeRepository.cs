
namespace ORM.Repositories
{
    using Domain;
    using ORM.Repositories.Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class EmployeeRepository : IRepository<Employee>
    {
        private TourContext _context;
        public EmployeeRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Employee Create(Employee employee)
        {
            this._context.Employee.Add(employee);
            this._context.SaveChanges();
            return employee;
        }
        public void Update(Employee employee)
        {
            _context.Set<Employee>().Update(employee);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            if (!this.TryGet(id, out var employee))
            {
                return;
            }
            this._context.Employee.Remove(employee);
            this._context.SaveChanges();
        }

        public IQueryable<Employee> Filter(Expression<Func<Employee, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<Employee> GetAll() => _context.Employee;

        public Employee GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out Employee employee)
        {
            employee = this.GetAll().SingleOrDefault(t => t.Id == id);
            return employee != null;
        }
    }
}
