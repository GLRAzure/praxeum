using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerUpdateByIdHandler : ILearnerHandler<LearnerUpdateById, LearnerUpdatedById>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerUpdateByIdHandler(
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

        public async Task<LearnerUpdatedById> ExecuteAsync(
            LearnerUpdateById learnerUpdateById)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerUpdateById.Id);

            if (learner == null)
            {
                throw new NullReferenceException($"Learner {learnerUpdateById.Id} does not exist.");
            }

            Mapper.Map(learnerUpdateById, learner);

            var microsoftProfile =
                _microsoftProfileRepository.FetchProfileAsync(learner.UserName);

            learner =
                Mapper.Map(microsoftProfile, learner);

            learner.ExpiresOn =
                DateTime.UtcNow.AddMinutes(
                    _learnerOptions.Value.CacheExpiresInMinutes);

            learner =
                await _learnerRepository.UpdateByIdAsync(
                    learnerUpdateById.Id,
                    learner);

            var learnerUpdatedById =
                Mapper.Map(learner, new LearnerUpdatedById());

            learnerUpdatedById.IsCached = false;

            return learnerUpdatedById;
        }
    }
}