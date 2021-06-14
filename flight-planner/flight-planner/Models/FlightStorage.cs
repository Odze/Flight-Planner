using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using WebGrease.Css;

namespace flight_planner.Models
{
    public static class FlightStorage
    {
        public static List<Flight> allFlights = new List<Flight>();
        public static HashSet<Airport> findedAirports = new HashSet<Airport>();
        private static List<string> addedAirports = new List<string>();
        private static int _id;
        public static List<Flight> FindedFlightsByReq = new List<Flight>();

        public static Flight AddFlight(Flight newflight)
        {
            newflight.id = _id;
            _id++;
            allFlights.Add(newflight);
            return newflight;
        }

        public static Flight FindFlight (int id)
        {
            return allFlights.FirstOrDefault(x => x.id == id);
        }

        public static HashSet<Airport> FindAirportWithName (string findFlight)
        {
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

            return findedAirports;
        }

        private static bool IsFindedInFrom (Flight flight, string findFlight)
        {
            int charsInFlight = findFlight.Length;
            int counter = 0;

            while (counter < charsInFlight)
            {
                if (flight.from.country.Trim().ToLower()[counter] != findFlight[counter])
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
                if (flight.from.city.Trim().ToLower()[counter] != findFlight[counter])
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
                if (flight.to.country.Trim().ToLower()[counter] != findFlight[counter])
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
                if (flight.to.city.Trim().ToLower()[counter] != findFlight[counter])
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