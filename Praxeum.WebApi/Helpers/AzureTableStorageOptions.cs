using System;

namespace Praxeum.WebApi.Helpers
{
    public class AzureTableStorageOptions
    {
        public string ConnectionString { get; set; }

        public AzureTableStorageOptions()
        {
           this.ConnectionString =
                Environment.GetEnvironmentVariable("AzureTableStorageConnectionString");
        }
    }
}
