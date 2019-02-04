using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserUpdater : IHandler<UserUpdate, UserUpdated>
    {
        private readonly IUserRepository _userRepository;

        public UserUpdater(
            IUserRepository userRepository)
        {
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

            Mapper.Map(userUpdate, user);

            user.LastModifiedOn =
                DateTime.UtcNow;

            user =
                await _userRepository.UpdateByIdAsync(
                    userUpdate.Id,
                    user);

            var userUpdated =
                Mapper.Map(user, new UserUpdated());

            userUpdated.IsCached = false;

            return userUpdated;
        }
    }
}