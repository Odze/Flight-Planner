using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    public class TestingApiController : ApiController
    {
        [Route ("testing-api/clear"),HttpPost]
        public  IHttpActionResult Clear ()
        {
            FlightStorage.allFlights.Clear();
            return Ok ();
        }
    }
}
