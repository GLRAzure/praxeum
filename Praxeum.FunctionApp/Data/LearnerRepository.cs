using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.FunctionApp.Helpers;
using Microsoft.Azure.Cosmos;

namespace Praxeum.FunctionApp.Data
{
    public class LearnerRepository : AzureCosmosDbRepository, ILearnerRepository
    {
        public LearnerRepository(
            AzureCosmosDbOptions azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<IEnumerable<Learner>> FetchListAsync()
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM l";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition, maxConcurrency: 2);

            var learnerList = new List<Learner>();

            while (learners.HasMoreResults)
            {
                learnerList.AddRange(
                    await learners.FetchNextSetAsync());
            };

            return learnerList;
        }

        public async Task<IEnumerable<Learner>> FetchListExpiredAsync(
            DateTime lastModifiedOn)
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM l WHERE l.lastModifiedOn <= @lastModifiedOn";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter(
                "@lastModifiedOn", lastModifiedOn);

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition, maxConcurrency: 2);

            var learnerList = new List<Learner>();

            while (learners.HasMoreResults)
            {
                learnerList.AddRange(
                    await learners.FetchNextSetAsync());
            };

            return learnerList;
        }

        public async Task<Learner> UpdateByIdAsync(
            Guid id,
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReplaceItemAsync<Learner>(
                    id.ToString(),
                    id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }
    }
}
