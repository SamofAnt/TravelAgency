﻿using Domain;
using ORM.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Repositories
{
    class AttractionRepository:IRepository<Attraction>
    {
        private TourContext _context;
        public AttractionRepository(TourContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public City Create(City city)
        {
            this._context.City.Add(city);
            this._context.SaveChanges();
            return city;
        }

        public void Delete(int id)
        {
            if (!this.TryGet(id, out var city))
            {
                return;
            }
            this._context.City.Remove(city);
            this._context.SaveChanges();
        }

        public IQueryable<City> Filter(Expression<Func<City, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public IQueryable<City> GetAll() => _context.City;

        public City GetById(int id)
        {
            return this.GetAll().SingleOrDefault(t => t.Id == id);
        }

        public bool TryGet(int id, out City city)
        {
            city = this.GetAll().SingleOrDefault(t => t.Id == id);
            return city != null;
        }
    }
}
