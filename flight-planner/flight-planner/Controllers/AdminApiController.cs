using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using flight_planner.Attributes;
using flight_planner.DbContext;
using flight_planner.Models;

namespace flight_planner.Controllers
{

    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private static readonly Object obj = new Object();

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights (int id)
        {
            lock (obj)
            {
                Flight flight;

                using (var ctx = new FlightPlannerDbContext())
                {
                    flight = ctx.Flights.Include(f => f.from).Include(f => f.to).SingleOrDefault(f => f.id == id);
                }

                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(flight);
            }
        }

        [Route("admin-api/flights/{id}"),HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            lock (obj)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    Flight flight = ctx.Flights.Include(f => f.from).Include(f => f.to).SingleOrDefault(f => f.id == id);
                    if (flight != null)
                    {
                        ctx.Flights.Remove(flight);
                    }

                    ctx.SaveChanges();
                }

                return Ok();
            }
        }

        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight (AddFlightRequest newFlight)
        {
            lock (obj)
            {
                if (IsWrongValues(newFlight) || IsSameAirports(newFlight) || !IsDateValid(newFlight))
                {
                    return BadRequest();
                }

                if (IsSameFlights(newFlight))
                {
                    return Conflict();
                }

                Flight output = new Flight();
                output.arrivalTime = newFlight.ArrivalTime;
                output.departureTime = newFlight.DepartureTime;
                output.from = newFlight.From;
                output.to = newFlight.To;
                output.carrier = newFlight.Carrier;

                using (var ctx = new FlightPlannerDbContext())
                {
                    ctx.Flights.Add(output);
                    ctx.SaveChanges();
                }

                return Created("", output);
            }
        }

        private bool IsWrongValues (AddFlightRequest newFlight)
        {
            if (string.IsNullOrEmpty(newFlight.Carrier) ||
                newFlight.To == null ||
                newFlight.From == null ||
                string.IsNullOrEmpty(newFlight.DepartureTime) ||
                string.IsNullOrEmpty(newFlight.ArrivalTime) ||
                string.IsNullOrEmpty(newFlight.To?.airport) ||
                string.IsNullOrEmpty(newFlight.To?.City) ||
                string.IsNullOrEmpty(newFlight.To?.Country) ||
                string.IsNullOrEmpty(newFlight.From?.airport) ||
                string.IsNullOrEmpty(newFlight.From?.City) ||
                string.IsNullOrEmpty(newFlight.From?.Country)
                )
            {
                return true;
            }
            return false;
        }

        private bool IsSameFlights (AddFlightRequest newflight)
        {
            List<Flight> FlightList = new List<Flight>();

            using (var ctx = new FlightPlannerDbContext())
            {
                FlightList = ctx.Flights.Include(f => f.from).Include(f => f.to).ToList();
            }

            if (FlightList.Count == 0)
            {
                return false;
            }

            foreach (var flight in FlightList)
            {
                if (
                    newflight.DepartureTime == flight.departureTime &&
                    newflight.ArrivalTime == flight.arrivalTime &&
                    newflight.To.airport.ToLower() == flight.to.airport.ToLower() &&
                    newflight.To.City.ToLower() == flight.to.City.ToLower() &&
                    newflight.To.Country.ToLower() == flight.to.Country.ToLower() &&
                    newflight.From.airport.ToLower() == flight.from.airport.ToLower() &&
                    newflight.From.City.ToLower() == flight.from.City.ToLower() &&
                    newflight.From.Country.ToLower() == flight.from.Country.ToLower()
                    )
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsSameAirports (AddFlightRequest newflight)
        {
            if (newflight.From.airport.ToLower().Trim() == newflight.To.airport.ToLower().Trim())
            {
                return true;
            }

            return false;
        }

        private bool IsDateValid (AddFlightRequest newflight)
        {
            var depatureTime = DateTime.Parse(newflight.DepartureTime);
            var arrivalTime = DateTime.Parse(newflight.ArrivalTime);

            return arrivalTime > depatureTime;
        }
    }
}
