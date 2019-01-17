using Microsoft.Extensions.DependencyInjection;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public static class ChallengeLearnerServices
    {
        public static void UseChallengeLearnerServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<ChallengeLearnerAdd, ChallengeLearnerAdded>, ChallengeLearnerAdder>();
            services.AddTransient<IHandler<ChallengeLearnerDelete, ChallengeLearnerDeleted>, ChallengeLearnerDeleter>();
            services.AddTransient<IHandler<ChallengeLearnerFetch, ChallengeLearnerFetched>, ChallengeLearnerFetcher>();
       }
    }
}
