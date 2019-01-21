using AutoMapper;
using Praxeum.FunctionApp.Data;

namespace Praxeum.FuncApp.Features.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<MicrosoftProfile, Learner>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.LastModifiedOn, o => o.Ignore());
        }
    }
}
