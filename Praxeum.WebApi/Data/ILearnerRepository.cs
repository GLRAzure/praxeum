using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Data
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
            int? maximumRecords = null,
            string orderBy = null);

        Task<IEnumerable<Learner>> FetchListAsync(
            Guid[] ids);

        Task<Learner> UpdateByIdAsync(
            Guid id,
            Learner learner);
    }
}
