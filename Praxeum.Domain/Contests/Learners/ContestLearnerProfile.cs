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
            CreateMap<ContestLearner, ContestLearnerUpdated>();
            CreateMap<ContestLearner, ContestLearnerProgressUpdated>();
            CreateMap<ContestLearnerAdd, ContestLearner>();
            CreateMap<MicrosoftProfile, ContestLearner>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.LastModifiedOn, o => o.Ignore());
            //CreateMap<MicrosoftProfileProgressStatus, ContestLearnerProgressStatus>();
            CreateMap<ContestLearnerUpdate, ContestLearner>();
            CreateMap<ContestLearnerProgressUpdate, ContestLearner>();
        }
    }
}
