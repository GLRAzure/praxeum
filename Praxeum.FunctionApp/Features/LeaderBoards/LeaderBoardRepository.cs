using Microsoft.Azure.Cosmos;
using Praxeum.FunctionApp.Data;
using Praxeum.FunctionApp.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardRepository : AzureCosmosDbRepository, ILeaderBoardRepository
    {
        public LeaderBoardRepository() : base(new AzureCosmosDbOptions())
        {
        }

        public LeaderBoardRepository(
            AzureCosmosDbOptions azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<LeaderBoard> AddAsync(
            LeaderBoard leaderBoard)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderBoards"];

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
               _cosmosDatabase.Containers["leaderBoards"];

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
                _cosmosDatabase.Containers["leaderBoards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReadItemAsync<LeaderBoard>(
                    id.ToString(),
                    id.ToString());

            return leaderBoardDocument.Resource;
        }

        public async Task<IEnumerable<LeaderBoard>> FetchListAsync()
        {
            var leaderBoardContainer =
                _cosmosDatabase.Containers["leaderBoards"];

            var query =
                $"SELECT * FROM lb";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var leaderBoards =
                leaderBoardContainer.Items.CreateItemQuery<LeaderBoard>(queryDefinition, maxConcurrency: 2);

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
               _cosmosDatabase.Containers["leaderBoards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReplaceItemAsync<LeaderBoard>(
                    id.ToString(),
                    id.ToString(),
                    leaderBoard);

            return leaderBoardDocument.Resource;
        }
    }
}
