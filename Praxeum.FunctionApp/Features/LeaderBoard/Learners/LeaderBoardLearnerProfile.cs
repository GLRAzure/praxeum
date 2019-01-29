using AutoMapper;
using Praxeum.Data;
using Praxeum.FunctionApp.Features.LeaderBoards.Learners;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LeaderBoardLearnerProfile : Profile
    {
        public LeaderBoardLearnerProfile()
        {
            CreateMap<LeaderBoardLearnerAdd, LeaderBoardLearner>();
       }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = 
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<LeaderBoardLearnerProfile>();
                });
            
            return 
                mapperConfiguration.CreateMapper();
        }
    }
}
