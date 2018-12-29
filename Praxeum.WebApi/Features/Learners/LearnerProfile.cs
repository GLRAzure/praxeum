using AutoMapper;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<Learner, LearnerDeletedById>();
            CreateMap<Learner, LearnerFetchedList>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, LearnerFetchedById>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<MicrosoftProfile, Learner>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.ExpiresOn, o => o.Ignore());
            CreateMap<MicrosoftProfile, LearnerAdded>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.IsCached, o => o.Ignore())
                .ForMember(d => d.ExpiresOn, o => o.Ignore());
            CreateMap<Learner, LearnerAdded>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            //CreateMap<LearnerUpdateById, Learner>()
            //    .ForMember(d => d.CreatedOn, o => o.Ignore());
            //CreateMap<Learner, LearnerUpdatedById>();
        }
    }
}
