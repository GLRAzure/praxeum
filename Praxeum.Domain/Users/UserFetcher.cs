using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserFetcher : IHandler<UserFetch, UserFetched>
    {
        private readonly IUserRepository _userRepository;

        public UserFetcher(
            IUserRepository userRepository)
        {
            _userRepository =
                userRepository;
        }

        public async Task<UserFetched> ExecuteAsync(
            UserFetch userFetch)
        {
            var user =
                await _userRepository.FetchByIdAsync(
                    userFetch.Id);

            var userFetched =
                Mapper.Map(user, new UserFetched());

            return userFetched;
        }
    }
}