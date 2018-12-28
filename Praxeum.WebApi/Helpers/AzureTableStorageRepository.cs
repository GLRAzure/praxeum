using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Praxeum.WebApi.Helpers
{
    public class AzureTableStorageRepository
    {

        // https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet

        protected readonly IOptions<AzureTableStorageOptions> _azureTableStorageOptions;
        private readonly CloudStorageAccount _cloudStorageAccount;
        protected readonly CloudTableClient _cloudTableClient;

        public AzureTableStorageRepository(
            IOptions<AzureTableStorageOptions> azureTableStorageOptions)
        {
            _azureTableStorageOptions =
                azureTableStorageOptions;

            _cloudStorageAccount =
                CloudStorageAccount.Parse(
                    _azureTableStorageOptions.Value.ConnectionString);

            _cloudTableClient =
                _cloudStorageAccount.CreateCloudTableClient();
        }
    }
}
