using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerFetchByIdHandler : ILearnerHandler<LearnerFetchById, LearnerFetchedById>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetchByIdHandler(
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

        public async Task<LearnerFetchedById> ExecuteAsync(
            LearnerFetchById learnerFetchById)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerFetchById.Id);

            bool isCached = true;

            if (learner.IsExpired)
            {
                var microsoftProfile =
                    _microsoftProfileRepository.FetchProfileAsync(learner.UserName);

                learner =
                    Mapper.Map(microsoftProfile, new Learner());

                learner.ExpiresOn =
                    DateTime.UtcNow.AddMinutes(
                        _learnerOptions.Value.CacheExpiresInMinutes);

                learner =
                    await _learnerRepository.UpdateByIdAsync(
                        learner.Id,
                        learner);

                isCached = false;
           }

            var learnerFetchedById =
                Mapper.Map(learner, new LearnerFetchedById());

            learnerFetchedById.IsCached = isCached;

            return learnerFetchedById;
        }
    }
}