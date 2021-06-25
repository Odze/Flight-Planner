using System.Data.Entity;
using flight_planner.Models;

namespace flight_planner.DbContext
{
    public class FlightPlannerDbContext : System.Data.Entity.DbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
} 