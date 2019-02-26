using AutoMapper;
using Praxeum.Data;
using System.Threading.Tasks;

namespace Praxeum.Domain.Users
{
    public class UserFetcherAdder : IHandler<UserFetchAdd, UserFetchedAdded>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserFetcherAdder(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper =
                mapper;
            _userRepository =
                userRepository;
        }

        public async Task<UserFetchedAdded> ExecuteAsync(
            UserFetchAdd userFetchAdd)
        {
            var user =
                await _userRepository.FetchByIdAsync(
                    userFetchAdd.Id);

            if (user == null)
            {
                user = new User(
                    userFetchAdd.Id);

                _mapper.Map(userFetchAdd, user);

                user = 
                    await _userRepository.AddAsync(
                        user);
            }

            var userFetchedAdded =
                _mapper.Map(user, new UserFetchedAdded());

            return userFetchedAdded;
        }
    }
}