using Microsoft.Extensions.DependencyInjection;
using Praxeum.Data;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.Contests
{
    public static class ContestServices
    {
        public static void UseContestServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<ContestAdd, ContestAdded>, ContestAdder>();
            services.AddTransient<IHandler<ContestDelete, ContestDeleted>, ContestDeleter>();
            services.AddTransient<IHandler<ContestFetch, ContestFetched>, ContestFetcher>();
            services.AddTransient<IHandler<ContestList, IEnumerable<ContestListed>>, ContestLister>();
            services.AddTransient<IHandler<ContestUpdate, ContestUpdated>, ContestUpdater>();
            
            services.AddTransient<IContestRepository, ContestRepository>();
       }
    }
}
