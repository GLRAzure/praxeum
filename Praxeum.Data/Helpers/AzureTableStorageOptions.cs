using System;

namespace Praxeum.Data.Helpers
{
    public class AzureTableStorageOptions
    {
        public string ConnectionString { get; set; }

        public AzureTableStorageOptions()
        {
            this.ConnectionString =
                Environment.GetEnvironmentVariable(
                    "AzureTableStorageOptions:ConnectionString");
        }
    }
}
