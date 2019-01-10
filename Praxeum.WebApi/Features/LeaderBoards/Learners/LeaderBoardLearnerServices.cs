using Microsoft.Extensions.DependencyInjection;
using Praxeum.WebApi.Data;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.LeaderBoards.Learners
{
    public static class LeaderBoardLearnerServices
    {
        public static void UseLeaderBoardLearnerServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded>, LeaderBoardLearnerAdder>();
            //services.AddTransient<IHandler<LeaderBoardLearnerDelete, LeaderBoardLearnerDeleted>, LeaderBoardLearnerDeleter>();
            services.AddTransient<IHandler<LeaderBoardLearnerFetch, LeaderBoardLearnerFetched>, LeaderBoardLearnerFetcher>();
       }
    }
}
