using Microsoft.Extensions.DependencyInjection;
using Praxeum.Data;
using System.Collections.Generic;

namespace Praxeum.Domain.LeaderBoards
{
    public static class LeaderBoardServices
    {
        public static void UseLeaderBoardServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<LeaderBoardAdd, LeaderBoardAdded>, LeaderBoardAdder>();
            services.AddTransient<IHandler<LeaderBoardDelete, LeaderBoardDeleted>, LeaderBoardDeleter>();
            services.AddTransient<IHandler<LeaderBoardFetch, LeaderBoardFetched>, LeaderBoardFetcher>();
            services.AddTransient<IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>>, LeaderBoardLister>();
            services.AddTransient<IHandler<LeaderBoardUpdate, LeaderBoardUpdated>, LeaderBoardUpdater>();
            
            services.AddTransient<ILeaderBoardRepository, LeaderBoardRepository>();
       }
    }
}
