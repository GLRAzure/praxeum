using Microsoft.Extensions.DependencyInjection;
using Praxeum.WebApi.Data;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.Learners
{
    public static class LearnerServices
    {
        public static void UseLearnerServices(
            this IServiceCollection services)
        {
            services.AddTransient<IHandler<LearnerAdd, LearnerAdded>, LearnerAdder>();
            services.AddTransient<IHandler<LearnerDelete, LearnerDeleted>, LearnerDeleter>();
            services.AddTransient<IHandler<LearnerFetch, LearnerFetched>, LearnerFetcher>();
            services.AddTransient<IHandler<LearnerList, IEnumerable<LearnerListed>>, LearnerLister>();
            services.AddTransient<IHandler<LearnerListAdd, LearnerListAdded>, LearnerListAdder>();
            services.AddTransient<IHandler<LearnerUpdate, LearnerUpdated>, LearnerUpdater>();

            services.AddTransient<ILearnerRepository, LearnerRepository>();
        }
    }
}
