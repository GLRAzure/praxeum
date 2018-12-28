using Praxeum.FunctionApp.Helpers;
using System;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerFetchByUserNameHandler
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetchByUserNameHandler()
        {
            _learnerRepository =
                new LearnerRepository();
        }

        public LearnerFetchByUserNameHandler(
            ILearnerRepository learnerRepository)
        {
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerFetchedByUserName> ExecuteAsync(
            LearnerFetchByUserName learnerFetchByUserName)
        {
            var learner =
                await _learnerRepository.FetchByUserNameAsync(
                    learnerFetchByUserName.UserName);

            var isCached = true;

            if (learner == null
                || learner.ModifiedOn == null
                || learner.ModifiedOn.AddMinutes(learnerFetchByUserName.CacheExpirationInMinutes) <= DateTime.UtcNow)
            {
                var microsoftProfileScraper =
                    new MicrosoftProfileScraper();

                learner =
                    microsoftProfileScraper.FetchProfile(
                        learnerFetchByUserName.UserName);
                learner.ModifiedOn =
                    DateTime.UtcNow;

                isCached = false;

                await _learnerRepository.AddOrUpdateAsync(learner);
            }

            var learnerFetchedByUserName =
                new LearnerFetchedByUserName(learner);

            learnerFetchedByUserName.IsCached = isCached;

            return learnerFetchedByUserName;
        }
    }
}