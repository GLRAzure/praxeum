using AutoMapper;
using Praxeum.Data;
using System.Threading.Tasks;

namespace Praxeum.Domain.Users
{
    public class UserAdder : IHandler<UserAdd, UserAdded>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserAdder(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper =
                mapper;
            _userRepository =
                userRepository;
        }

        public async Task<UserAdded> ExecuteAsync(
            UserAdd userAdd)
        {
            var user =
                _mapper.Map(userAdd, new User());

            user = 
                await _userRepository.AddAsync(
                    user);

            var userAdded =
                _mapper.Map(user, new UserAdded());

            return userAdded;
        }
    }
}