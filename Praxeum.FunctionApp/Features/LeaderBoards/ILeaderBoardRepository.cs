using Praxeum.FunctionApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public interface ILeaderBoardRepository 
    {
        Task<LeaderBoard> AddAsync(
            LeaderBoard leaderBoard);

        Task<LeaderBoard> DeleteByIdAsync(
            Guid id);

        Task<LeaderBoard> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<LeaderBoard>> FetchListAsync();

        Task<LeaderBoard> UpdateByIdAsync(
            Guid id,
            LeaderBoard leaderBoard);
    }
}
