using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner.Core.Models;
using flight_planner.Core.Services;
using flight_planner.Data;

namespace flight_planner.Services
{
    public class EntityService <T> : DbService, IEntityService<T> where T : Entity
    { 
        public EntityService(flight_plannerDBContext context) : base(context)
        {

        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public IEnumerable<T> Get()
        {
            return Get<T>();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public void Create(T entity)
        { 
            Create<T>(entity);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }
    }
}
