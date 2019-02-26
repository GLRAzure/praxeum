using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestProfile : Profile
    {
        public ContestProfile()
        {
            CreateMap<Contest, ContestDeleted>();
            CreateMap<Contest, ContestListed>();
            CreateMap<Contest, ContestFetched>();
            CreateMap<ContestAdd, Contest>()
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Contest, ContestAdded>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<ContestUpdate, Contest>()
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Contest, ContestUpdated>();
            CreateMap<Contest, ContestProgressUpdated>();
        }
    }
}
