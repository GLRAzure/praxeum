using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IContestLearnerRepository
    {
        Task<ContestLearner> AddAsync(
            Guid contestId,
            ContestLearner contestLearner);

        Task<ContestLearner> DeleteByIdAsync(
            Guid contestId,
            Guid id);

        Task<ContestLearner> FetchByIdAsync(
            Guid contestId,
            Guid id);

        Task<ContestLearner> FetchByUserNameAsync(
            Guid contestId,
            string userName);

        Task<IEnumerable<ContestLearner>> FetchListAsync(
            Guid contestId);

        Task<ContestLearner> UpdateByIdAsync(
            Guid contestId,
            Guid id,
            ContestLearner contestLearner);
    }
}
