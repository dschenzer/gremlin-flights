using System.Collections.Generic;
using Microsoft.Azure.CosmosDB.BulkExecutor.Graph.Element;
using System.Linq;

namespace gremlin_import
{
    public class Utils
    {
        public IEnumerable<GremlinVertex> GenerateRoutesVertices(IEnumerable<Routes> routes)
        {
            List<GremlinVertex> vertices = new List<GremlinVertex>();

            var destinctSourceAirports = routes.Select(a => a.SourceAirport).Distinct();
            var destinctDestinationAirports = routes.Select(a => a.DestinationAirport).Distinct();

            foreach (var sAirport in destinctSourceAirports)
            {
                Routes airport = routes.Where(a => a.SourceAirport == sAirport).First();

                GremlinVertex airportVertex = new GremlinVertex(airport.SourceAirportID, airport.SourceAirport);
                airportVertex.AddProperty("SourceAirpor", airport.SourceAirport);                   

                vertices.Add(airportVertex);          
            }

            /*
            foreach (var dAirport in destinctDestinationAirports)
            {
                Routes airport = routes.Where(a => a.SourceAirport == dAirport).First();

                GremlinVertex airportVertex = new GremlinVertex(airport.DestinationAirportID, airport.DestinationAirport);      

                vertices.Add(airportVertex);          
            } */

            return vertices;
        }
 
        public IEnumerable<GremlinEdge> GenerateRoutesEdges(IEnumerable<Routes> routes)
        {
            List<GremlinEdge> edges = new List<GremlinEdge>();

            foreach (Routes route in routes)
            {
                GremlinEdge routeEdge = new GremlinEdge(
                    route.SourceAirportID + "-" + route.DestinationAirportID, 
                    route.SourceAirport + "-" + route.DestinationAirport, 
                    route.SourceAirportID, route.DestinationAirportID,
                    route.SourceAirport, route.DestinationAirport);
                    routeEdge.AddProperty(nameof(route.Airline), route.Airline);
                    routeEdge.AddProperty(nameof(route.AirlineID), route.AirlineID);
                    routeEdge.AddProperty(nameof(route.Codeshare), route.Codeshare);
                    routeEdge.AddProperty(nameof(route.Equipment), route.Equipment);
                    routeEdge.AddProperty(nameof(route.Stops), route.Stops);

                edges.Add(routeEdge);    
            }

            return edges;
        } 
    }
}