using Microsoft.Extensions.DependencyInjection;
using Praxeum.Data;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.Challenges
{
    public static class ChallengeServices
    {
        public static void UseChallengeServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<ChallengeAdd, ChallengeAdded>, ChallengeAdder>();
            services.AddTransient<IHandler<ChallengeDelete, ChallengeDeleted>, ChallengeDeleter>();
            services.AddTransient<IHandler<ChallengeFetch, ChallengeFetched>, ChallengeFetcher>();
            services.AddTransient<IHandler<ChallengeList, IEnumerable<ChallengeListed>>, ChallengeLister>();
            services.AddTransient<IHandler<ChallengeUpdate, ChallengeUpdated>, ChallengeUpdater>();
            
            services.AddTransient<IChallengeRepository, ChallengeRepository>();
       }
    }
}
