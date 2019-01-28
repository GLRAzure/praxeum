using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Data
{
    public interface ILearnerRepository
    {
        Task<Learner> AddAsync(
            Learner learner);

        Task<Learner> AddOrUpdateAsync(
            Learner learner);

        Task<Learner> DeleteByIdAsync(
            Guid id);

        Task<Learner> FetchByIdAsync(
            Guid id);

        Task<Learner> FetchByUserNameAsync(
            string userName);

        Task<IEnumerable<Learner>> FetchListAsync(
            string status = null,
            int? maximumRecords = null,
            string orderBy = null);

        Task<IEnumerable<Learner>> FetchListAsync(
            Guid[] ids);

        Task<Learner> UpdateByIdAsync(
            Guid id,
            Learner learner);
    }
}
