using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;

namespace Praxeum.Data
{
    public class LeaderBoardRepository : AzureCosmosDbRepository, ILeaderBoardRepository
    {
        public LeaderBoardRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<LeaderBoard> AddAsync(
       LeaderBoard leaderBoard)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.CreateItemAsync<LeaderBoard>(
                    leaderBoard.Id.ToString(),
                    leaderBoard);

            return leaderBoardDocument.Resource;
        }

        public async Task<LeaderBoard> DeleteByIdAsync(
            Guid id)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.DeleteItemAsync<LeaderBoard>(
                    id.ToString(),
                    id.ToString());

            return leaderBoardDocument.Resource;
        }

        public async Task<LeaderBoard> FetchByIdAsync(
            Guid id)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReadItemAsync<LeaderBoard>(
                    id.ToString(),
                    id.ToString());

            return leaderBoardDocument.Resource;
        }

        public async Task<IEnumerable<LeaderBoard>> FetchListAsync()
        {
            var leaderBoardContainer =
                _cosmosDatabase.Containers["leaderboards"];

            var query =
                $"SELECT * FROM lb ORDER BY lb.name";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var leaderBoards =
                leaderBoardContainer.Items.CreateItemQuery<LeaderBoard>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var leaderBoardList = new List<LeaderBoard>();

            while (leaderBoards.HasMoreResults)
            {
                leaderBoardList.AddRange(
                    await leaderBoards.FetchNextSetAsync());
            };

            return leaderBoardList;
        }

        public async Task<LeaderBoard> UpdateByIdAsync(
            Guid id,
            LeaderBoard leaderBoard)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReplaceItemAsync<LeaderBoard>(
                    id.ToString(),
                    id.ToString(),
                    leaderBoard);

            return leaderBoardDocument.Resource;
        }
    }
}
