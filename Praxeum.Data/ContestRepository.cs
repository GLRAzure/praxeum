using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class ContestRepository : AzureCosmosDbRepository, IContestRepository
    {
        public ContestRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<Contest> AddAsync(
            Contest challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["contests"];

            var challengeDocument =
                await challengeContainer.Items.CreateItemAsync<Contest>(
                    challenge.Id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }

        public async Task<Contest> AddOrUpdateAsync(
            Contest challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["contests"];

            var challengeDocument =
                await challengeContainer.Items.UpsertItemAsync<Contest>(
                    challenge.Id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }

        public async Task<Contest> DeleteByIdAsync(
            Guid id)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["contests"];

            var challengeDocument =
                await challengeContainer.Items.DeleteItemAsync<Contest>(
                    id.ToString(),
                    id.ToString());

            return challengeDocument.Resource;
        }

        public async Task<Contest> FetchByIdAsync(
            Guid id)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["contests"];

            var challengeDocument =
                await challengeContainer.Items.ReadItemAsync<Contest>(
                    id.ToString(),
                    id.ToString());

            return challengeDocument.Resource;
        }

        public async Task<IEnumerable<Contest>> FetchListAsync()
        {
            var challengeContainer =
                _cosmosDatabase.Containers["contests"];

            var query =
                $"SELECT * FROM l";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var contests =
                challengeContainer.Items.CreateItemQuery<Contest>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var challengeList = new List<Contest>();

            while (contests.HasMoreResults)
            {
                challengeList.AddRange(
                    await contests.FetchNextSetAsync());
            };

            return challengeList;
        }

        public async Task<Contest> UpdateByIdAsync(
            Guid id,
            Contest challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["contests"];

            var challengeDocument =
                await challengeContainer.Items.ReplaceItemAsync<Contest>(
                    id.ToString(),
                    id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }
    }
}
