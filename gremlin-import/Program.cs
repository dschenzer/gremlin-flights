using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Azure.CosmosDB.BulkExecutor;
using Microsoft.Azure.CosmosDB.BulkExecutor.BulkImport;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace gremlin_import
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TextReader csvTextReader = File.OpenText("..\\..\\routes.csv");
            
            CsvReader csvReader = new CsvReader(csvTextReader);
            csvReader.Configuration.RegisterClassMap<RoutesMap>();
    
            var routesRecord = csvReader.GetRecords<Routes>();
            
            ConnectionPolicy connectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            };

            DocumentClient client = new DocumentClient(
                new Uri("https://gremlin-flights-data.documents.azure.com:443/"),
                "RZ1dawlAne57M0KBw6Kb11pvimpF6ZByW8RwurQjkSXbtjpI0PhFWxpRKW5n8ntrhEZHmUjvNuM02zJlxX2dRw==",
                connectionPolicy);
     
            DocumentCollection routesCollection = client.CreateDocumentCollectionQuery(
                UriFactory.CreateDatabaseUri("flights")).AsEnumerable().Where(d => d.Id == "routes").SingleOrDefault();

            IBulkExecutor bulkExecutor = new BulkExecutor(client, routesCollection);
            await bulkExecutor.InitializeAsync();

            client.ConnectionPolicy.RetryOptions.MaxRetryWaitTimeInSeconds = 0;
            client.ConnectionPolicy.RetryOptions.MaxRetryAttemptsOnThrottledRequests = 0;

            Utils utils = new Utils();

            BulkImportResponse vResponse = await bulkExecutor.BulkImportAsync(utils.GenerateRoutesVertices(routesRecord.ToList()), enableUpsert:true);

            Console.WriteLine("NumberOfDocumentsImported: " + vResponse.NumberOfDocumentsImported);

            foreach (var item in routesRecord)
            {
                System.Console.WriteLine(item);

                break;
            }

            Console.WriteLine("Hello World!");
        }
    }
}
