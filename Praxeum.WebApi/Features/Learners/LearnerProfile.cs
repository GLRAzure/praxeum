using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<Learner, LearnerDeleted>();
            CreateMap<Learner, LearnerListed>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, LearnerFetched>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, LearnerAdded>();
        }
    }
}
