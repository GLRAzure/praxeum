using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class LearnerRepository : AzureCosmosDbRepository, ILearnerRepository
    {
        public LearnerRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<Learner> AddAsync(
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.CreateItemAsync<Learner>(
                    learner.Id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }

        public async Task<Learner> AddOrUpdateAsync(
            Learner learner)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.UpsertItemAsync<Learner>(
                    learner.Id.ToString(),
                    learner);

            return learnerDocument.Resource;
        }

        public async Task<Learner> DeleteByIdAsync(
            Guid id)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.DeleteItemAsync<Learner>(
                    id.ToString(),
                    id.ToString());

            return learnerDocument.Resource;
        }

        public async Task<Learner> FetchByIdAsync(
            Guid id)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReadItemAsync<Learner>(
                    id.ToString(),
                    id.ToString());

            return learnerDocument.Resource;
        }

        public async Task<Learner> FetchByUserNameAsync(
            string userName)
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM l where l.userName = @userName";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter(
                "@userName", userName);

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition,
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var learnerList = new List<Learner>();

            while (learners.HasMoreResults)
            {
                learnerList.AddRange(
                    await learners.FetchNextSetAsync());
            };

            var learner =
                learnerList.FirstOrDefault();

            return learner;
        }

        public async Task<IEnumerable<Learner>> FetchListAsync(
            string status = null,
            int? maximumRecords = null,
            string orderBy = "l.displayName")
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT";

            if (maximumRecords.HasValue)
            {
                query += $" TOP {maximumRecords.Value}";
            }

            query += " * FROM l";

            if (!string.IsNullOrWhiteSpace(status))
            {
                query += " WHERE l.status = @status";
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query += $" ORDER BY {orderBy}";
            }

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            if (!string.IsNullOrWhiteSpace(status))
            {
                queryDefinition.UseParameter("@status", status);
            }

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition,
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var learnerList = new List<Learner>();

            while (learners.HasMoreResults)
            {
                learnerList.AddRange(
                    await learners.FetchNextSetAsync());
            };

            return learnerList;
        }

        public async Task<IEnumerable<Learner>> FetchListAsync(
            Guid[] ids)
        {
            var learnerContainer =
                _cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM l";

            query += " WHERE ARRAY_CONTAINS(@ids, l.id)";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter("@ids", ids);

            var learners =
                learnerContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition,
                    _azureCosmosDbOptions.Value.MaxConcurrency);

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
