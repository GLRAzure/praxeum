using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;
using Microsoft.WindowsAzure.Storage.Table;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardRepository : AzureTableStorageRepository, ILeaderBoardRepository
    {
        public LeaderBoardRepository(
            IOptions<AzureTableStorageOptions> azureTableStorageOptions) : base(azureTableStorageOptions)
        {
        }

        public async Task<LeaderBoard> AddAsync(
            LeaderBoard leaderBoard)
        {
            var cloudTable =
                _cloudTableClient.GetTableReference("leaderboards");

            var tableOperation =
                TableOperation.Insert(leaderBoard);

            var tableResult =
                await cloudTable.ExecuteAsync(
                    tableOperation);

            leaderBoard =
                (LeaderBoard)tableResult.Result;

            return leaderBoard;
        }

        public Task<LeaderBoard> DeleteByIdAsync(
            Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaderBoard> FetchByIdAsync(
            Guid id)
        {
            var cloudTable =
                _cloudTableClient.GetTableReference("leaderboards");

            var tableOperation =
                TableOperation.Retrieve<LeaderBoard>(id.ToString(), id.ToString());

            var tableResult =
                await cloudTable.ExecuteAsync(
                    tableOperation);

            var leaderBoard =
                (LeaderBoard)tableResult.Result;

            return leaderBoard;
        }

        public async Task<IEnumerable<LeaderBoard>> FetchListAsync()
        {
            var cloudTable =
                _cloudTableClient.GetTableReference("leaderboards");

            var tableQuery =
                new TableQuery<LeaderBoard>();

            TableContinuationToken continuationToken = null;

            var leaderBoardList =
                new List<LeaderBoard>();

            do
            {
                var tableQueryResult =
                    await cloudTable.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                leaderBoardList.AddRange(
                    tableQueryResult.Results);

                continuationToken = 
                    tableQueryResult.ContinuationToken;

            } while (continuationToken != null);

            return leaderBoardList;
        }

        public Task<LeaderBoard> UpdateByIdAsync(
            Guid id,
            LeaderBoard leaderBoard)
        {
            throw new NotImplementedException();
        }
    }
}
