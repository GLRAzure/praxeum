using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IChallengeRepository
    {
        Task<Challenge> AddAsync(
            Challenge challenge);

        Task<Challenge> AddOrUpdateAsync(
            Challenge challenge);

        Task<Challenge> DeleteByIdAsync(
            Guid id);

        Task<Challenge> FetchByIdAsync(
            Guid id);

        Task<Challenge> FetchByUserNameAsync(
            string userName);

        Task<IEnumerable<Challenge>> FetchListAsync();

        Task<IEnumerable<Challenge>> FetchListAsync(
            Guid[] ids);

        Task<Challenge> UpdateByIdAsync(
            Guid id,
            Challenge challenge);
    }
}
