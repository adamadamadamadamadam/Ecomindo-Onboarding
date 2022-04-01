using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Threading.Tasks;

namespace Messager
{
    public class Program
    {
        // connection string to the Event Hubs namespace
        private const string connectionString = "Endpoint=sb://onboarding-evennthub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O3fUmlNPjHkPGLzna3yLYsk5k2IMTn8vPwSqW7XMbOA=";

        // name of the event hub
        private const string eventHubName = "onboarding-topic";

        static async Task Main(string[] args)
        {
            var producerClient = new EventHubProducerClient(connectionString, eventHubName);

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
