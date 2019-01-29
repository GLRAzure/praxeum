using AutoMapper;
using Praxeum.Data;
using Praxeum.FunctionApp.Features.LeaderBoards.Learners;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<Learner, LearnerAdded>();
            CreateMap<MicrosoftProfile, Learner>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName.Trim().ToLower()))
                .ForMember(d => d.Rank, o => o.MapFrom(s => (s.ProgressStatus.CurrentLevel * 1000000) + s.ProgressStatus.CurrentLevelPointsEarned + s.ProgressStatus.TotalPoints))
                .ForMember(d => d.Id, o => o.Ignore())                
                .ForMember(d => d.LastModifiedOn, o => o.Ignore());
            CreateMap<LeaderBoardLearnerAdd, LeaderBoardLearner>();
       }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = 
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<LearnerProfile>();
                });
            
            return 
                mapperConfiguration.CreateMapper();
        }
    }
}
