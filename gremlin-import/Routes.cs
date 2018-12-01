namespace gremlin_import
{
    public class Routes
    {
        public string Airline { get; set; }
        public string AirlineID { get; set; }
        public string SourceAirport { get; set; }
        public string SourceAirportID { get; set; }
        public string DestinationAirport { get; set; }
        public string DestinationAirportID { get; set; }
        public string Codeshare { get; set; }
        public string Stops { get; set; }
        public string Equipment { get; set; }

        public override string ToString()
        {
            return SourceAirport + " " + DestinationAirport + " - " + Airline; 
        }    
    }
}