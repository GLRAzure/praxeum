using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            //CreateMap<ContestLearner, ContestLearnerDeleted>();
            CreateMap<Learner, ContestLearnerAdded>();
            CreateMap<Learner, ContestLearnerFetched>();
            CreateMap<ContestLearnerAdd, ContestLearner>();
        }
    }
}
