using System;

namespace Praxeum.WebApi.Helpers
{
    public class AzureCosmosDbOptions
    {
        public string DatabaseId { get; set; }
        public string ConnectionString { get; set; }

        public AzureCosmosDbOptions()
        {
            this.DatabaseId =
                Environment.GetEnvironmentVariable("AzureCosmosDBDatabaseId");
            this.ConnectionString =
                Environment.GetEnvironmentVariable("AzureCosmosDBConnectionString");
        }
    }
}
