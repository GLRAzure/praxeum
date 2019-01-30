using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Data
{
    public interface ILearnerLeaderBoardRepository
    {
        Task<LearnerLeaderBoard> AddAsync(
            Guid learnerId,
            Guid leaderBoardId);

        Task<LearnerLeaderBoard> DeleteAsync(
            Guid learnerId,
            Guid leaderBoardId);

        Task<LearnerLeaderBoard> UpdateAsync(
            Guid learnerId,
            Guid leaderBoardId);
    }
}
