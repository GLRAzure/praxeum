using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserFetched>();
            CreateMap<User, UserAdded>();
            CreateMap<User, UserFetchedAdded>();
            CreateMap<UserAdd, User>();
            CreateMap<UserFetchAdd, User>();
        }
    }
}
