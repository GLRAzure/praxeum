using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.Data;

namespace Praxeum.Domain.Learners
{
    public class LearnerUpdater : IHandler<LearnerUpdate, LearnerUpdated>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerUpdater(
            IOptions<LearnerOptions> learnerOptions,
            ILearnerRepository learnerRepository)
        {
            _learnerOptions =
                learnerOptions;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerUpdated> ExecuteAsync(
            LearnerUpdate learnerUpdate)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerUpdate.Id);

            if (learner == null)
            {
                throw new NullReferenceException($"Learner {learnerUpdate.Id} does not exist.");
            }

            Mapper.Map(learnerUpdate, learner);

            learner.LastModifiedOn =
                DateTime.UtcNow;

            learner =
                await _learnerRepository.UpdateByIdAsync(
                    learnerUpdate.Id,
                    learner);

            var learnerUpdated =
                Mapper.Map(learner, new LearnerUpdated());

            learnerUpdated.IsCached = false;

            return learnerUpdated;
        }
    }
}