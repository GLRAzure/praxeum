using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IUserRepository
    {
        Task<User> AddAsync(
            User user);

        Task<User> FetchByIdAsync(
            string id);

        Task<IEnumerable<User>> FetchListAsync();

        Task<User> UpdateByIdAsync(
            string id,
            User user);
    }
}
