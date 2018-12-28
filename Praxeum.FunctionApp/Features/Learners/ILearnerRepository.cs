using Praxeum.FunctionApp.Data;
using System;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public interface ILearnerRepository
    {
        Task<Learner> AddAsync(
            Learner learner);

        Task<Learner> FetchByIdAsync(
            Guid id);

        Task<Learner> FetchByUserNameAsync(
            string userName);
        
        Task<Learner> AddOrUpdateAsync(
            Learner learner);
    }
}
