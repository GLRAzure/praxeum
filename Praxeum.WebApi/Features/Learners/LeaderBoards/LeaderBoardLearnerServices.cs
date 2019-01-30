using Microsoft.Extensions.DependencyInjection;

namespace Praxeum.WebApi.Features.Learners.LeaderBoards
{
    public static class LearnerLeaderBoardServices
    {
        public static void UseLearnerLeaderBoardServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded>, LearnerLeaderBoardAdder>();
            services.AddTransient<IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted>, LearnerLeaderBoardDeleter>();
       }
    }
}
