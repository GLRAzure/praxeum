using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserAdded>();
            CreateMap<User, UserFetched>();
            CreateMap<User, UserFetchedAdded>();
            CreateMap<User, UserListed>();
            CreateMap<UserAdd, User>();
            CreateMap<UserFetchAdd, User>();
            CreateMap<UserFetched, UserUpdate>()
                .ForMember(d => d.Id, o => o.Ignore());
            CreateMap<UserUpdate, User>();
        }
    }
}
