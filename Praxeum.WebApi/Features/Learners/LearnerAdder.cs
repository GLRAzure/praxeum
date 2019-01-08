using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerAdder : IHandler<LearnerAdd, LearnerAdded>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileFetcher _microsoftProfileFetcher;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerAdder(
            IOptions<LearnerOptions> learnerOptions,
            IMicrosoftProfileFetcher microsoftProfileFetcher,
            ILearnerRepository learnerRepository)
        {
            _learnerOptions =
                learnerOptions;
            _microsoftProfileFetcher =
                microsoftProfileFetcher;
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
                await _microsoftProfileFetcher.FetchProfileAsync(learnerAdd.Name);

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