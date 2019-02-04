using AutoMapper;
using Praxeum.Data;
using System.Threading.Tasks;

namespace Praxeum.Domain.Users
{
    public class UserAdder : IHandler<UserAdd, UserAdded>
    {
        private readonly IUserRepository _userRepository;

        public UserAdder(
            IUserRepository userRepository)
        {
            _userRepository =
                userRepository;
        }

        public async Task<UserAdded> ExecuteAsync(
            UserAdd userAdd)
        {
            var user =
                Mapper.Map(userAdd, new User());

            user = 
                await _userRepository.AddAsync(
                    user);

            var userAdded =
                Mapper.Map(user, new UserAdded());

            return userAdded;
        }
    }
}