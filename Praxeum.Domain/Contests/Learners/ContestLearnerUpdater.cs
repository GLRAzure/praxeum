using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerUpdater : IHandler<ContestLearnerUpdate, ContestLearnerUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly IContestLearnerTargetValueUpdater _contestLearnerTargetValueUpdater;
        private readonly IContestLearnerCurrentValueUpdater _contestLearnerCurrentValueUpdater;

        public ContestLearnerUpdater(
            IMapper mapper,
            IEventPublisher eventPublisher,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository,
            IContestLearnerTargetValueUpdater contestLearnerTargetValueUpdater,
            IContestLearnerCurrentValueUpdater contestLearnerCurrentValueUpdater)
        {
            _mapper =
                mapper;
            _eventPublisher =
                eventPublisher;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
            _contestLearnerTargetValueUpdater =
                contestLearnerTargetValueUpdater;
            _contestLearnerCurrentValueUpdater =
                contestLearnerCurrentValueUpdater;
        }

        public async Task<ContestLearnerUpdated> ExecuteAsync(
            ContestLearnerUpdate contestLearnerUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerUpdate.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerUpdate.ContestId} not found");
            }

            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerUpdate.ContestId,
                    contestLearnerUpdate.Id);

           if (contestLearner == null)
            {
                throw new ArgumentOutOfRangeException(
                    $"Contest learner {contestLearnerUpdate.UserName} not found");
            }

            contestLearner.UserName =
                contestLearnerUpdate.UserName.ToLower();
            contestLearner.StartValue = 
                contestLearnerUpdate.StartValue;

            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(
                    contestLearner.UserName);

            contestLearner =
                _contestLearnerTargetValueUpdater.Update(
                    contest,
                    contestLearner);

            if (contest.IsStatus(ContestStatus.InProgress))
            {
                contestLearner =
                    _contestLearnerCurrentValueUpdater.Update(
                        contest,
                        contestLearner,
                        microsoftProfile);
            }

            contestLearner =
                await _contestLearnerRepository.UpdateByIdAsync(
                    contestLearnerUpdate.ContestId,
                    contestLearnerUpdate.Id,
                    contestLearner);

            var contestLearnerUpdated =
                _mapper.Map(contestLearner, new ContestLearnerUpdated());

            await _eventPublisher.PublishAsync("contestlearner.updated", contestLearnerUpdated);

            return contestLearnerUpdated;
        }
    }
}