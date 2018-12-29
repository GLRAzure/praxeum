using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerAddHandler : ILearnerHandler<LearnerAdd, LearnerAdded>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerAddHandler(
            IOptions<LearnerOptions> learnerOptions,
            IMicrosoftProfileRepository microsoftProfileRepository,
            ILearnerRepository learnerRepository)
        {
            _learnerOptions =
                learnerOptions;
            _microsoftProfileRepository =
                microsoftProfileRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerAdded> ExecuteAsync(
            LearnerAdd learnerAdd)
        {
            var learner =
                await _learnerRepository.FetchByUserNameAsync(learnerAdd.Name);

            if (learner == null)
            {
                learner = new Learner
                {
                    Id = learnerAdd.Id
                };
            }

            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(learnerAdd.Name);

            learner =
                Mapper.Map(microsoftProfile, learner);

            learner.ExpiresOn =
                DateTime.UtcNow.AddMinutes(
                    _learnerOptions.Value.CacheExpiresInMinutes);

            learner =
                await _learnerRepository.AddOrUpdateAsync(
                    learner);

            var learnerAdded =
                Mapper.Map(learner, new LearnerAdded());

            learnerAdded.IsCached = false;

            return learnerAdded;
        }
    }
}