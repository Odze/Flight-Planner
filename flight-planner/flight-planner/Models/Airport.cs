using Newtonsoft.Json;

namespace flight_planner.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string airport { get; set; }
    }
}