namespace Praxeum.WebApp.Helpers
{
    public class AzureQueueStorageEventPublisherOptions
    {
        public string ConnectionString { get;  set; }
        public string QueuePrefix { get;  set; }
    }
}