using Microsoft.Extensions.Options;
using Praxeum.Data;
using Praxeum.Data.Helpers;

namespace Praxeum.FunctionApp.Helpers
{
    public class ObjectFactory
    {
        private static IOptions<AzureCosmosDbOptions> _azureCosmosDbOptions;

        public static IOptions<AzureCosmosDbOptions> CreateAzureCosmosDbOptions()
        {
            if (_azureCosmosDbOptions == null)
            {
                _azureCosmosDbOptions =
                    Options.Create(new AzureCosmosDbOptions());
            }

            return _azureCosmosDbOptions;
        }

        public static ContestRepository CreateContestRepository()
        {
            return new ContestRepository(
                ObjectFactory.CreateAzureCosmosDbOptions());
        }

        public static ContestLearnerRepository CreateContestLearnerRepository()
        {
            return new ContestLearnerRepository(
                ObjectFactory.CreateAzureCosmosDbOptions());
        }
    } 
}
