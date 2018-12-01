using CsvHelper.Configuration;

namespace gremlin_import
{
    public class RoutesMap : ClassMap<Routes>
    {
        public RoutesMap()
        {
            Map(m => m.Airline).Name("Airline");
            Map(m => m.AirlineID).Name("Airline ID");
            Map(m => m.SourceAirport).Name("SourceAirport");
            Map(m => m.SourceAirportID).Name("Source airport ID");
            Map(m => m.DestinationAirport).Name("DestinationAirport");
            Map(m => m.DestinationAirportID).Name("Destination airport ID");
            Map(m => m.Codeshare).Name("Codeshare");
            Map(m => m.Stops).Name("Stops");
            Map(m => m.Equipment).Name("Equipment");

        }
    }
}