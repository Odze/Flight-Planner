using System.Web.Http;

namespace flight_planner.Controllers
{
    public class TestingApiController : ApiController
    {
        /*
        [Route ("testing-api/clear"),HttpPost]
        public  IHttpActionResult Clear ()
        {
            using (var ctx = new FlightPlannerDbContext())
            {
               
                ctx.Flights.RemoveRange(ctx.Flights);
                ctx.Airports.RemoveRange(ctx.Airports);
                ctx.SaveChanges();
            }

            return Ok ();
        }
        */
    }
}
