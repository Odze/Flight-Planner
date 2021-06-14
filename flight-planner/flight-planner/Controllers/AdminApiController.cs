using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using flight_planner.Attributes;
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
                var flight = FlightStorage.FindFlight(id);
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
                var flight = FlightStorage.FindFlight(id);

                if (flight != null)
                {
                    FlightStorage.allFlights.Remove(flight);
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
                FlightStorage.AddFlight(output);

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
                string.IsNullOrEmpty(newFlight.To?.city) ||
                string.IsNullOrEmpty(newFlight.To?.country) ||
                string.IsNullOrEmpty(newFlight.From?.airport) ||
                string.IsNullOrEmpty(newFlight.From?.city) ||
                string.IsNullOrEmpty(newFlight.From?.country)
                )
            {
                return true;
            }
            return false;
        }

        private bool IsSameFlights (AddFlightRequest newflight)
        {
            foreach (var flight in FlightStorage.allFlights)
            {
                if (
                    newflight.DepartureTime == flight.departureTime &&
                    newflight.ArrivalTime == flight.arrivalTime &&
                    newflight.To.airport == flight.to.airport &&
                    newflight.To.city == flight.to.city &&
                    newflight.To.country == flight.to.country &&
                    newflight.From.airport == flight.from.airport &&
                    newflight.From.city == flight.from.city &&
                    newflight.From.country == flight.from.country
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
