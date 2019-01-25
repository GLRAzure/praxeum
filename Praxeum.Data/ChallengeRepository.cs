using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class ChallengeRepository : AzureCosmosDbRepository, IChallengeRepository
    {
        public ChallengeRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<Challenge> AddAsync(
            Challenge challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["challenges"];

            var challengeDocument =
                await challengeContainer.Items.CreateItemAsync<Challenge>(
                    challenge.Id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }

        public async Task<Challenge> AddOrUpdateAsync(
            Challenge challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["challenges"];

            var challengeDocument =
                await challengeContainer.Items.UpsertItemAsync<Challenge>(
                    challenge.Id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }

        public async Task<Challenge> DeleteByIdAsync(
            Guid id)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["challenges"];

            var challengeDocument =
                await challengeContainer.Items.DeleteItemAsync<Challenge>(
                    id.ToString(),
                    id.ToString());

            return challengeDocument.Resource;
        }

        public async Task<Challenge> FetchByIdAsync(
            Guid id)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["challenges"];

            var challengeDocument =
                await challengeContainer.Items.ReadItemAsync<Challenge>(
                    id.ToString(),
                    id.ToString());

            return challengeDocument.Resource;
        }

        public async Task<Challenge> FetchByUserNameAsync(
            string userName)
        {
            var challengeContainer =
                _cosmosDatabase.Containers["challenges"];

            var query =
                $"SELECT * FROM l where l.userName = @userName";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter(
                "@userName", userName);

            var challenges =
                challengeContainer.Items.CreateItemQuery<Challenge>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var challengeList = new List<Challenge>();

            while (challenges.HasMoreResults)
            {
                challengeList.AddRange(
                    await challenges.FetchNextSetAsync());
            };

            var challenge =
                challengeList.FirstOrDefault();

            return challenge;
        }

        public async Task<IEnumerable<Challenge>> FetchListAsync()
        {
            var challengeContainer =
                _cosmosDatabase.Containers["challenges"];

            var query =
                $"SELECT * FROM l";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var challenges =
                challengeContainer.Items.CreateItemQuery<Challenge>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var challengeList = new List<Challenge>();

            while (challenges.HasMoreResults)
            {
                challengeList.AddRange(
                    await challenges.FetchNextSetAsync());
            };

            return challengeList;
        }

        public async Task<IEnumerable<Challenge>> FetchListAsync(
            Guid[] ids)
        {
            var challengeContainer =
                _cosmosDatabase.Containers["challenges"];

            var query =
                $"SELECT * FROM l";

            query += " WHERE ARRAY_CONTAINS(@ids, l.id)";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter("@ids", ids);

            var challenges =
                challengeContainer.Items.CreateItemQuery<Challenge>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var challengeList = new List<Challenge>();

            while (challenges.HasMoreResults)
            {
                challengeList.AddRange(
                    await challenges.FetchNextSetAsync());
            };

            return challengeList;
        }

        public async Task<Challenge> UpdateByIdAsync(
            Guid id,
            Challenge challenge)
        {
            var challengeContainer =
               _cosmosDatabase.Containers["challenges"];

            var challengeDocument =
                await challengeContainer.Items.ReplaceItemAsync<Challenge>(
                    id.ToString(),
                    id.ToString(),
                    challenge);

            return challengeDocument.Resource;
        }
    }
}
