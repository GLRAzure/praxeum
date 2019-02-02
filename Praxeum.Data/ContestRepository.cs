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
            Contest contest)
        {
            var contestContainer =
               _cosmosDatabase.Containers["contests"];

            var contestDocument =
                await contestContainer.Items.CreateItemAsync<Contest>(
                    contest.Id.ToString(),
                    contest);

            return contestDocument.Resource;
        }

        public async Task<Contest> AddOrUpdateAsync(
            Contest contest)
        {
            var contestContainer =
               _cosmosDatabase.Containers["contests"];

            var contestDocument =
                await contestContainer.Items.UpsertItemAsync<Contest>(
                    contest.Id.ToString(),
                    contest);

            return contestDocument.Resource;
        }

        public async Task<Contest> DeleteByIdAsync(
            Guid id)
        {
            var contestContainer =
               _cosmosDatabase.Containers["contests"];

            var contestDocument =
                await contestContainer.Items.DeleteItemAsync<Contest>(
                    id.ToString(),
                    id.ToString());

            return contestDocument.Resource;
        }

        public async Task<Contest> FetchByIdAsync(
            Guid id)
        {
            var contestContainer =
               _cosmosDatabase.Containers["contests"];

            var contestDocument =
                await contestContainer.Items.ReadItemAsync<Contest>(
                    id.ToString(),
                    id.ToString());

            return contestDocument.Resource;
        }

        public async Task<IEnumerable<Contest>> FetchListAsync()
        {
            var contestContainer =
                _cosmosDatabase.Containers["contests"];

            var query =
                $"SELECT * FROM l";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var contests =
                contestContainer.Items.CreateItemQuery<Contest>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var contestList = new List<Contest>();

            while (contests.HasMoreResults)
            {
                contestList.AddRange(
                    await contests.FetchNextSetAsync());
            };

            return contestList;
        }

        public async Task<Contest> UpdateByIdAsync(
            Guid id,
            Contest contest)
        {
            var contestContainer =
               _cosmosDatabase.Containers["contests"];

            var contestDocument =
                await contestContainer.Items.ReplaceItemAsync<Contest>(
                    id.ToString(),
                    id.ToString(),
                    contest);

            return contestDocument.Resource;
        }
    }
}
