using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            CreateMap<Challenge, ChallengeDeleted>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<Challenge, ChallengeListed>();
            CreateMap<Challenge, ChallengeFetched>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<ChallengeAdd, Challenge>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Challenge, ChallengeAdded>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<ChallengeUpdate, Challenge>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<Challenge, ChallengeUpdated>()
                .ForMember(d => d.Learners, o => o.Ignore());
        }
    }
}
