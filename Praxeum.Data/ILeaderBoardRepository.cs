using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Data
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
