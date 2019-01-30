using AutoMapper;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LeaderBoardLearnerProfile : Profile
    {
        public LeaderBoardLearnerProfile()
        {
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
