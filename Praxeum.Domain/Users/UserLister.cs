using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserLister : IHandler<UserList, IEnumerable<UserListed>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserLister(
            IMapper mapper,
            IUserRepository userFetchListRepository)
        {
            _mapper =
                mapper;
            _userRepository =
                userFetchListRepository;
        }

        public async Task<IEnumerable<UserListed>> ExecuteAsync(
            UserList userList)
        {
            var users =
                await _userRepository.FetchListAsync();

            var userListed =
                users.Select(x => _mapper.Map(x, new UserListed()));

            userListed = 
                userListed.OrderBy(x => x.Name);

            return userListed;
        }
    }
}