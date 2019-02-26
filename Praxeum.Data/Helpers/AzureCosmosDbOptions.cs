using Microsoft.Extensions.Options;
using System;

namespace Praxeum.Data.Helpers
{
    public class AzureCosmosDbOptions
    {
        public string DatabaseId { get; set; }
        public string ConnectionString { get; set; }
        public int MaxConcurrency { get; set; }

        public AzureCosmosDbOptions()
        {
            this.DatabaseId =
                Environment.GetEnvironmentVariable(
                    "AzureCosmosDbOptions:DatabaseId");
            this.ConnectionString =
                Environment.GetEnvironmentVariable(
                    "AzureCosmosDbOptions:ConnectionString");
            this.MaxConcurrency = 4;
        }

        public IOptions<AzureCosmosDbOptions> AsOptions()
        {
            return Options.Create(this);
        }
    }
}
