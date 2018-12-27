using Microsoft.Azure.Cosmos;

namespace Praxeum.FunctionApp.Helpers
{
    public abstract class AzureCosmosDbRepository 
    {
        private readonly AzureCosmosDbOptions _azureCosmosDbOptions;
        private readonly CosmosClient _cosmosClient;
        protected readonly CosmosDatabase _cosmosDatabase;

        public AzureCosmosDbRepository(
            AzureCosmosDbOptions azureCosmosDbOptions)
        {
            _azureCosmosDbOptions =
                azureCosmosDbOptions;

            _cosmosClient =
                new CosmosClient(
                    _azureCosmosDbOptions.ConnectionString);
            _cosmosDatabase = _cosmosClient.Databases[_azureCosmosDbOptions.DatabaseId];
        }
    }
}
