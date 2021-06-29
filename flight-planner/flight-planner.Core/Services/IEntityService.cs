using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner.Core.Models;

namespace flight_planner.Core.Services
{
    public interface IEntityService<T> where T: Entity
    {
        IQueryable<T> Query();
        IEnumerable<T> Get();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
