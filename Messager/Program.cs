using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Messager
{
    public class Program
    {
        // connection string to the Event Hubs namespace
        private static string _connectionString;

        // name of the event hub
        private static string _eventHubName;

        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            _connectionString = config["EventHub:ConnectionString"];
            _eventHubName = config["EventHub:EventHubName"];
            var producerClient = new EventHubProducerClient(_connectionString, _eventHubName);

            var eventBatch = await producerClient.CreateBatchAsync();

            Console.Write("how many messages:");
            var answer = Console.ReadLine();

            try
            {
                var x = int.Parse(answer);
                
                Console.WriteLine("input messages");

                for (int i = 0; i < x; i++)
                {
                    var message = Console.ReadLine();
                    eventBatch.TryAdd(new EventData(message));
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("Events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
    }
}
