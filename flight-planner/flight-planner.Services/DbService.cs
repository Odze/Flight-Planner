using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using flight_planner.Core.Models;
using flight_planner.Core.Services;
using flight_planner.Data;

namespace flight_planner.Services
{
    public class DbService : IDbService
    {
        protected private readonly flight_plannerDBContext _context;

        public DbService(flight_plannerDBContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Get<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public void Create<T>(T entity) where T : Entity
        { 
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
