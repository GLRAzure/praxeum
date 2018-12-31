using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Learners
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

        Task<IEnumerable<Learner>> FetchListAsync();

        Task<Learner> UpdateByIdAsync(
            Guid id,
            Learner learner);
    }
}
