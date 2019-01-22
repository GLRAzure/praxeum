using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Data
{
    public interface ILearnerRepository
    {
        Task<IEnumerable<Learner>> FetchListAsync();

        Task<IEnumerable<Learner>> FetchListExpiredAsync(
            DateTime lastModifiedOn);

        Task<Learner> UpdateByIdAsync(
            Guid id,
            Learner learner);
    }
}
