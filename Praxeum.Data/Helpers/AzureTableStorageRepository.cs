using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Praxeum.Data.Helpers
{
    public class AzureTableStorageRepository
    {
        protected readonly IOptions<AzureTableStorageOptions> _azureTableStorageOptions;
        protected readonly CloudTableClient _cloudTableClient;
        protected CloudTable _cloudTable;

        public AzureTableStorageRepository(
            IOptions<AzureTableStorageOptions> azureTableStorageOptions)
        {
            _azureTableStorageOptions =
                azureTableStorageOptions;

            var cloudStorageAccount = CloudStorageAccount.Parse(
                azureTableStorageOptions.Value.ConnectionString);
        
            _cloudTableClient =
                cloudStorageAccount.CreateCloudTableClient();
        }
    }
}
