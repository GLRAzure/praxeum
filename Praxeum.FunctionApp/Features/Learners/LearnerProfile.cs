using AutoMapper;
using Praxeum.FunctionApp.Data;

namespace Praxeum.FuncApp.Features.Learners
{
    public class LearnerProfile : Profile
    {
        public LearnerProfile()
        {
            CreateMap<MicrosoftProfile, Learner>()
                .ForMember(d => d.Rank, o => o.MapFrom(s => s.ProgressStatus.CurrentLevelPointsEarned + s.ProgressStatus.TotalPoints))
                .ForMember(d => d.Id, o => o.Ignore())                
                .ForMember(d => d.LastModifiedOn, o => o.Ignore());
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
