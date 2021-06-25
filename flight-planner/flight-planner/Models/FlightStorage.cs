using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using flight_planner.DbContext;

namespace flight_planner.Models
{
    public static class FlightStorage
    {
        public static List<Flight> allFlights = new List<Flight>();
        public static HashSet<Airport> findedAirports = new HashSet<Airport>();
        private static List<string> addedAirports = new List<string>();
        private static int _id;
        public static List<Flight> FindedFlightsByReq = new List<Flight>();

        public static HashSet<Airport> FindAirportWithName (string findFlight)
        {
            using (var ctx = new FlightPlannerDbContext())
            {
                allFlights = ctx.Flights.Include(f => f.from).Include(f => f.to).ToList();
            }

            findedAirports.Clear();
            addedAirports.Clear();
            findFlight = findFlight.Trim().ToLower();

            foreach(var flight in allFlights)
            {
                if (IsFindedInFrom(flight, findFlight) && !(addedAirports.Contains(flight.from.airport.Trim().ToLower())) )
                {
                    findedAirports.Add(flight.from);
                    addedAirports.Add(flight.from.airport.Trim().ToLower());
                }

                if (IsFindedInTo(flight,findFlight) && !(addedAirports.Contains(flight.to.airport.Trim().ToLower())) )
                {
                    findedAirports.Add(flight.to);
                    addedAirports.Add(flight.to.airport.Trim().ToLower());
                }
            }

            allFlights.Clear();
            return findedAirports;
        }

        private static bool IsFindedInFrom (Flight flight, string findFlight)
        {
            int charsInFlight = findFlight.Length;
            int counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.from.Country.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.from.City.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.from.airport.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            return false;
        }

        private static bool IsFindedInTo(Flight flight, string findFlight)
        {
            int charsInFlight = findFlight.Length;
            int counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.to.Country.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.to.City.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.to.airport.Trim().ToLower()[counter] != findFlight[counter])
                {
                    break;
                }

                if (counter == charsInFlight - 1)
                {
                    return true;
                }

                counter++;
            }

            return false;
        }

        public static List<Flight> FindFlightWithReq (SearchFlightsRequest flightsRequest)
        {
            using (var ctx = new FlightPlannerDbContext())
            {
                allFlights = ctx.Flights.Include(f => f.from).Include(f => f.to).ToList();
            }

            string flightDate;
            string flightsReqDate;
            flightsReqDate = flightsRequest.DepartureDate;
            FindedFlightsByReq.Clear();
            string from = flightsRequest.From.Trim().ToLower();
            string to = flightsRequest.To.Trim().ToLower();
            
            foreach (var flight in allFlights)
            {
                flightDate = flight.departureTime.Trim();
                flightDate = flightDate.Substring(0, flightDate.Length - 6);

                if (flightDate == flightsReqDate &&
                    from == flight.from.airport.Trim().ToLower() &&
                    to == flight.to.airport.Trim().ToLower() &&
                    !IsNullFligtFromToAndFLightReq(flight,flightsRequest)
                    )
                {
                    FindedFlightsByReq.Add(flight);
                }
            }

            allFlights.Clear();
            return FindedFlightsByReq;
        }

        private static bool IsNullFligtFromToAndFLightReq(Flight flight, SearchFlightsRequest searchReq)
        {
            if (flight.from.airport == null ||
                flight.to.airport == null ||
                flight.departureTime == null ||
                searchReq.From == null ||
                searchReq.To == null ||
                searchReq.DepartureDate == null
            )
            {
                return true;
            }

            return false;
        }
    }
}