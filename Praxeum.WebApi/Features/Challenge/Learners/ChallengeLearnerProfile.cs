using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerProfile : Profile
    {
        public ChallengeLearnerProfile()
        {
            //CreateMap<ChallengeLearner, ChallengeLearnerDeleted>();
            CreateMap<Learner, ChallengeLearnerAdded>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, ChallengeLearnerFetched>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<ChallengeLearnerAdd, ChallengeLearner>();
        }
    }
}
