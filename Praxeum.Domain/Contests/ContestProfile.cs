using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestProfile : Profile
    {
        public ContestProfile()
        {
            CreateMap<Contest, ContestDeleted>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<Contest, ContestListed>();
            CreateMap<Contest, ContestFetched>();
            CreateMap<ContestAdd, Contest>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Contest, ContestAdded>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<ContestUpdate, Contest>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Contest, ContestUpdated>()
                .ForMember(d => d.Learners, o => o.Ignore());
        }
    }
}
