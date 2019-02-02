using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<Learner, LearnerDeleted>();
            CreateMap<Learner, LearnerListed>();
            CreateMap<Learner, LearnerFetched>();
            CreateMap<Learner, LearnerAdded>();
        }
    }
}
