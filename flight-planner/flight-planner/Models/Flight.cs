namespace flight_planner.Models
{
    public class Flight
    {
        public int id { get; set; }
        public Airport from { get; set; }
        public Airport to { get; set; } 
        public string carrier { get; set; }
        public string departureTime { get; set; }
        public string arrivalTime { get; set; }

    }
}