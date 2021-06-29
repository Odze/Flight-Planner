using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    public class CustomerFlightApiController : ApiController
    {
        /*
        private static readonly Object obj = new Object();

        [Route("api/airports"), HttpGet]
        public IHttpActionResult searchAirports(string search)
        {
            FlightStorage.FindAirportWithName(search);            
            var FindedAirports = FlightStorage.findedAirports;

            if (FindedAirports.Count == 0)
            {
                return NotFound();
            }

            return Ok(FindedAirports);
        }

        [Route("api/flights/search")]
        public IHttpActionResult searchFlights(SearchFlightsRequest searchFlightsRequest)
        {
            lock (obj)
            {
                if (IsFReqNull(searchFlightsRequest))
                {
                    return BadRequest();
                }

                FlightStorage.FindFlightWithReq(searchFlightsRequest);
                var findedFlights = FlightStorage.FindedFlightsByReq;

                var pageResult = new PageResult(findedFlights);
                return Ok(pageResult);
            }
        }

        [Route("api/flights/{id}"),HttpGetAttribute]
        public IHttpActionResult findFlightById(int id)
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

        private bool IsFReqNull(SearchFlightsRequest searchReq)
        {
            if (searchReq == null)
            {
                return true;
            }

            if (searchReq.DepartureDate == null ||
                searchReq.From == null ||
                searchReq.To == null)
            {
                return true;
            }

            if (searchReq.To.Trim().ToLower() == searchReq.From.Trim().ToLower())
            {
                return true;
            }

            return false;
        }
        */
    }
}
