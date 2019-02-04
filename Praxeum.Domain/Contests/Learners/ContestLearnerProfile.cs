using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            CreateMap<Learner, ContestLearnerAdded>();
            CreateMap<ContestLearnerAdd, ContestLearner>();
        }
    }
}
