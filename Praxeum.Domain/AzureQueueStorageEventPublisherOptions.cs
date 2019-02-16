using Microsoft.Extensions.Options;
using System;

namespace Praxeum.Domain
{
    public class AzureQueueStorageEventPublisherOptions
    {
        public string ConnectionString { get;  set; }
        public string QueuePrefix { get;  set; }

        public AzureQueueStorageEventPublisherOptions()
        {
            this.ConnectionString =
                Environment.GetEnvironmentVariable(
                    "AzureQueueStorageEventPublisherOptions:ConnectionString");
            this.QueuePrefix =
                Environment.GetEnvironmentVariable(
                    "AzureQueueStorageEventPublisherOptions:QueuePrefix");
        }

        public IOptions<AzureQueueStorageEventPublisherOptions> AsOptions()
        {
            return Options.Create(this);
        }
    }
}