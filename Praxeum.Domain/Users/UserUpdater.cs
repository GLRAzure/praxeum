using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserUpdater : IHandler<UserUpdate, UserUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserUpdater(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper =
                mapper;
            _userRepository =
                userRepository;
        }

        public async Task<UserUpdated> ExecuteAsync(
            UserUpdate userUpdate)
        {
            var user =
                await _userRepository.FetchByIdAsync(
                    userUpdate.Id);

            if (user == null)
            {
                throw new NullReferenceException($"User {userUpdate.Id} does not exist.");
            }

            _mapper.Map(userUpdate, user);

            user =
                await _userRepository.UpdateByIdAsync(
                    userUpdate.Id,
                    user);

            var userUpdated =
                _mapper.Map(user, new UserUpdated());

            return userUpdated;
        }
    }
}