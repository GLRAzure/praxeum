using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerListUpdater : IHandler<LearnerListUpdate, LearnerListUpdated>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerListUpdater(
            ILogger logger,
            IMapper mapper,
            IMicrosoftProfileRepository microsoftProfileRepository,
            ILearnerRepository learnerRepository)
        {
            _logger =
                logger;
            _mapper =
               mapper;
            _microsoftProfileRepository =
                 microsoftProfileRepository;
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
                try
                {
                    var microsoftProfile =
                        await _microsoftProfileRepository.FetchProfileAsync(learner.UserName);

                    _logger.LogInformation(
                        JsonConvert.SerializeObject(microsoftProfile));

                    _mapper.Map(microsoftProfile, learner);

                    learner.Status = LearnerStatus.Imported;
                    learner.StatusMessage = string.Empty;

                    _logger.LogInformation(
                        $"Imported!");
                }
                catch (Exception ex)
                {
                    learner.Status = LearnerStatus.Failed;
                    learner.StatusMessage = ex.Message;

                    _logger.LogInformation(
                        $"Failed to import '{learner.UserName}': {ex.Message}.");
                }

                learner.LastModifiedOn = DateTime.UtcNow;

                var learnerUpdated =
                    await _learnerRepository.UpdateByIdAsync(
                        learner.Id,
                        learner);

                learnerListUpdated.NumberLearnersUpdated++;
            }

            return learnerListUpdated;
        }
    }
}