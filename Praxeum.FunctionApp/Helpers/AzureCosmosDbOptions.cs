using System;

namespace Praxeum.FunctionApp.Helpers
{
    public class AzureCosmosDbOptions
    {
        // https://medium.com/statuscode/getting-key-vault-secrets-in-azure-functions-37620fd20a0b

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
