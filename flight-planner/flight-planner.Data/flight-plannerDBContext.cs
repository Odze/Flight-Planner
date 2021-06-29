using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner.Core.Models;

namespace flight_planner.Data
{
     public class flight_plannerDBContext : DbContext
    {
        public flight_plannerDBContext() : base("flight-planner")
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
