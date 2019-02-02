using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Praxeum.Domain;

namespace Praxeum.WebApp.Helpers
{
    public class AzureQueueStorageEventPublisher : IEventPublisher
    {
        private readonly AzureQueueStorageEventPublisherOptions _options;

        public AzureQueueStorageEventPublisher(
            IOptions<AzureQueueStorageEventPublisherOptions> options)
        {
            _options = options.Value;
        }

        // https://azure.microsoft.com/en-us/blog/azure-storage-client-library-retry-policy-recommendations/
        // https://docs.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific
        // https://docs.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific#azure-storage

        public async Task PublishAsync(
            string type,
            object data)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(
                _options.ConnectionString);

            var cloudQueueName =
               $"{type.ToLower()}".Replace(".", "-");

            if (!string.IsNullOrWhiteSpace(_options.QueuePrefix))
            {
                cloudQueueName = $"{_options.QueuePrefix}-{cloudQueueName}";
            }

            var cloudQueueClient =
                cloudStorageAccount.CreateCloudQueueClient();

            var cloudQueue =
                cloudQueueClient.GetQueueReference(cloudQueueName);

            await cloudQueue.CreateIfNotExistsAsync();

            await cloudQueue.AddMessageAsync(
                new CloudQueueMessage(
                    JsonConvert.SerializeObject(data)));
        }

        public async Task PublishAsync(
            string[] types,
            object data)
        {
            foreach (var type in types)
            {
                await this.PublishAsync(type, data);
            }
        }
    }
}