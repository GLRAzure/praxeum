namespace Praxeum.WebApi.Helpers
{
    public class AzureQueueStorageEventPublisherOptions
    {
        public string QueuePrefix { get; set; }
        public string ConnectionString { get; set; }
    }
}
