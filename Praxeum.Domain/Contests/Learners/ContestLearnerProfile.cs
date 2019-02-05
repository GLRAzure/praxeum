using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            CreateMap<ContestLearner, ContestLearnerAdded>();
            CreateMap<ContestLearner, ContestLearnerDeleted>();
            CreateMap<ContestLearner, ContestLearnerFetched>();
            CreateMap<ContestLearnerAdd, ContestLearner>();
        }
    }
}
