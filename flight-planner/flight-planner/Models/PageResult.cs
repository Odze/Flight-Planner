using System;
using System.Collections.Generic;

namespace flight_planner.Models
{
    public class PageResult
    {
        public int Page;
        public int TotalItems;
        public List<Flight> Items = new List<Flight>();

        public PageResult(List<Flight> flightList)
        {
            Items = flightList;
            TotalItems = flightList.Count;
            Page = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(flightList.Count) / 10));
        }
    }
}