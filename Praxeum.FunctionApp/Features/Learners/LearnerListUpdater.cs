using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.FunctionApp.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerListUpdater : IHandler<LearnerListUpdate, LearnerListUpdated>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMicrosoftProfileScraper _microsoftProfileScraper;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerListUpdater(
            ILogger logger,
            IMapper mapper,
            IMicrosoftProfileScraper microsoftProfileScraper,
            ILearnerRepository learnerRepository)
        {
            _logger =
                logger;
            _mapper =
               mapper;
            _microsoftProfileScraper =
                 microsoftProfileScraper;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerListUpdated> ExecuteAsync(
            LearnerListUpdate learnerListUpdate)
        {
            var learners =
                await _learnerRepository.FetchListAsync();

            var learnerListUpdated =
                new LearnerListUpdated();

            foreach (var learner in learners)
            {
                var microsoftProfile =
                    await _microsoftProfileScraper.FetchProfileAsync(learner.UserName);

                _logger.LogInformation(
                    JsonConvert.SerializeObject(microsoftProfile));

                _mapper.Map(microsoftProfile, learner);

                learner.LastModifiedOn =
                    DateTime.UtcNow;

                _logger.LogInformation(
                    $"Updating '{microsoftProfile.UserName}'...");

                var learnerUpdated =
                    await _learnerRepository.UpdateByIdAsync(
                        learner.Id,
                        learner);

                _logger.LogInformation(
                    JsonConvert.SerializeObject(learnerUpdated));

                _logger.LogInformation(
                    $"Updated.");

                learnerListUpdated.NumberLearnersUpdated++;
            }

            return learnerListUpdated;
        }
    }
}