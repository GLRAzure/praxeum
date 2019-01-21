using System;

namespace Praxeum.FunctionApp.Helpers
{
    public class AzureCosmosDbOptions
    {
        public string DatabaseId { get; set; }
        public string ConnectionString { get; set; }

        public AzureCosmosDbOptions()
        {
            this.DatabaseId =
                Environment.GetEnvironmentVariable("AzureCosmosDbOptions:DatabaseId");
            this.ConnectionString =
                Environment.GetEnvironmentVariable("AzureCosmosDbOptions:ConnectionString");
       }
    }
}
