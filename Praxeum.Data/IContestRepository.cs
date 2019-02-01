using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IContestRepository
    {
        Task<Contest> AddAsync(
            Contest challenge);

        Task<Contest> AddOrUpdateAsync(
            Contest challenge);

        Task<Contest> DeleteByIdAsync(
            Guid id);

        Task<Contest> FetchByIdAsync(
            Guid id);

        Task<IEnumerable<Contest>> FetchListAsync();

        Task<Contest> UpdateByIdAsync(
            Guid id,
            Contest challenge);
    }
}
