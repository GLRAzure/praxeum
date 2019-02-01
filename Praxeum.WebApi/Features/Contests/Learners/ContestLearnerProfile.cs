using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            //CreateMap<ContestLearner, ContestLearnerDeleted>();
            CreateMap<Learner, ContestLearnerAdded>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, ContestLearnerFetched>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<ContestLearnerAdd, ContestLearner>();
        }
    }
}
