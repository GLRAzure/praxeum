using Microsoft.Extensions.DependencyInjection;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    public static class LeaderBoardLearnerServices
    {
        public static void UseLeaderBoardLearnerServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded>, LeaderBoardLearnerAdder>();
            services.AddTransient<IHandler<LeaderBoardLearnerDelete, LeaderBoardLearnerDeleted>, LeaderBoardLearnerDeleter>();
            services.AddTransient<IHandler<LeaderBoardLearnerFetch, LeaderBoardLearnerFetched>, LeaderBoardLearnerFetcher>();
       }
    }
}
