using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            CreateMap<Learner, ContestLearnerAdded>();
            CreateMap<ContestLearner, ContestLearnerFetched>()
                .ForMember(d => d.DisplayName, o => o.Ignore())
                .ForMember(d => d.UserName, o => o.Ignore());
            CreateMap<ContestLearnerAdd, ContestLearner>();
        }
    }
}
