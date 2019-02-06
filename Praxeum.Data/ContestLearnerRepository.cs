using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class ContestLearnerRepository : AzureCosmosDbRepository, IContestLearnerRepository
    {
        public ContestLearnerRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<ContestLearner> AddAsync(
            Guid contestId,
            ContestLearner contestLearner)
        {
            var contestLearnerContainer =
               _cosmosDatabase.Containers["contestlearners"];

            var contestLearnerDocument =
                await contestLearnerContainer.Items.CreateItemAsync<ContestLearner>(
                    contestId.ToString(),
                    contestLearner);

            return contestLearnerDocument.Resource;
        }

        public async Task<ContestLearner> DeleteByIdAsync(
            Guid contestId,
            Guid id)
        {
            var contestLearnerContainer =
               _cosmosDatabase.Containers["contestlearners"];

            var contestLearnerDocument =
                await contestLearnerContainer.Items.DeleteItemAsync<ContestLearner>(
                    contestId.ToString(),
                    id.ToString());

            return contestLearnerDocument.Resource;
        }

        public async Task<ContestLearner> FetchByIdAsync(
            Guid contestId,
            Guid id)
        {
            var contestLearnerContainer =
               _cosmosDatabase.Containers["contestlearners"];

            var contestLearnerDocument =
                await contestLearnerContainer.Items.ReadItemAsync<ContestLearner>(
                    contestId.ToString(),
                    id.ToString());

            return contestLearnerDocument.Resource;
        }

        public async Task<ContestLearner> FetchByUserNameAsync(
            Guid contestId,
            string userName)
        {
            var contestLearnerContainer =
                _cosmosDatabase.Containers["contestlearners"];

            var query =
                $"SELECT * FROM cl WHERE cl.userName = @userName";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter("@userName", userName.ToLower());

            var contestLearners =
                contestLearnerContainer.Items.CreateItemQuery<ContestLearner>(
                    queryDefinition,
                   contestId.ToString());

            var contestLearnerList = new List<ContestLearner>();

            while (contestLearners.HasMoreResults)
            {
                contestLearnerList.AddRange(
                    await contestLearners.FetchNextSetAsync());
            };

            var contestLearner =
                contestLearnerList.FirstOrDefault();

            return contestLearner;
        }

        public async Task<IEnumerable<ContestLearner>> FetchListAsync(
            Guid contestId)
        {
            var contestLearnerContainer =
                _cosmosDatabase.Containers["contestlearners"];

            var query =
                $"SELECT * FROM cl";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var contestLearners =
                contestLearnerContainer.Items.CreateItemQuery<ContestLearner>(
                    queryDefinition,
                   contestId.ToString());

            var contestLearnerList = new List<ContestLearner>();

            while (contestLearners.HasMoreResults)
            {
                contestLearnerList.AddRange(
                    await contestLearners.FetchNextSetAsync());
            };

            return contestLearnerList;
        }

        public async Task<ContestLearner> UpdateByIdAsync(
            Guid contestId,
            Guid id,
            ContestLearner contestLearner)
        {
            var contestLearnerContainer =
               _cosmosDatabase.Containers["contestLearners"];

            var contestLearnerDocument =
                await contestLearnerContainer.Items.ReplaceItemAsync<ContestLearner>(
                    contestId.ToString(),
                    id.ToString(),
                    contestLearner);

            return contestLearnerDocument.Resource;
        }
    }
}
