using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    public class CustomerFlightApiController : ApiController
    {
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
            if (IsFReqNull(searchFlightsRequest))
            {
                return BadRequest();
            }

            FlightStorage.FindFlightWithReq(searchFlightsRequest);
            var findedFlights = FlightStorage.FindedFlightsByReq;

            var pageResult = new PageResult(findedFlights);
            return Ok(pageResult);
        }

        [Route("api/flights/{id}"),HttpGetAttribute]
        public IHttpActionResult findFlightById(int id)
        {
            var flight = FlightStorage.FindFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
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
    }
}
