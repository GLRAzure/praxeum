using Microsoft.Extensions.DependencyInjection;

namespace Praxeum.Domain.Contests.Learners
{
    public static class ContestLearnerServices
    {
        public static void UseContestLearnerServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<ContestLearnerAdd, ContestLearnerAdded>, ContestLearnerAdder>();
            services.AddTransient<IHandler<ContestLearnerDelete, ContestLearnerDeleted>, ContestLearnerDeleter>();
            services.AddTransient<IHandler<ContestLearnerFetch, ContestLearnerFetched>, ContestLearnerFetcher>();
       }
    }
}
