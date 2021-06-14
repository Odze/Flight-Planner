using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flight_planner.Models
{
    public class SearchFlightsRequest
    {
        public string From;
        public string To;
        public string DepartureDate;
    }
}