using AutoMapper;
using Praxeum.Data;
using Praxeum.FunctionApp.Features.LeaderBoards.Learners;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerLeaderBoardProfile : Profile
    {
        public LearnerLeaderBoardProfile()
        {
            CreateMap<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded>();
       }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = 
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<LearnerLeaderBoardProfile>();
                });
            
            return 
                mapperConfiguration.CreateMapper();
        }
    }
}
