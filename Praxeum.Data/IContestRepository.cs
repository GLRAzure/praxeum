using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IContestRepository
    {
        Task<Contest> AddAsync(
            Contest contest);

        Task<Contest> AddOrUpdateAsync(
            Contest contest);

        Task<Contest> DeleteByIdAsync(
            Guid id);

        Task<Contest> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<Contest>> FetchListAsync(
            string status);

        Task<Contest> UpdateByIdAsync(
            Guid id,
            Contest contest);
    }
}
