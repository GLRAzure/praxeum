using AutoMapper;
using Praxeum.Data;
using System;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly IContestLearnerStartValueUpdater _contestLearnerStartValueUpdater;
        private readonly IContestLearnerTargetValueUpdater _contestLearnerTargetValueUpdater;
        private readonly IContestLearnerCurrentValueUpdater _contestLearnerCurrentValueUpdater;

        public ContestLearnerAdder(
            IMapper mapper,
            IEventPublisher eventPublisher,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository,
            IContestLearnerStartValueUpdater contestLearnerStartValueUpdater,
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
            _contestLearnerStartValueUpdater =
                contestLearnerStartValueUpdater;
            _contestLearnerTargetValueUpdater =
                contestLearnerTargetValueUpdater;
            _contestLearnerCurrentValueUpdater =
                contestLearnerCurrentValueUpdater;
        }

        public async Task<ContestLearnerAdded> ExecuteAsync(
            ContestLearnerAdd contestLearnerAdd)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerAdd.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerAdd.ContestId} not found");
            }

            contestLearnerAdd.UserName =
                contestLearnerAdd.UserName.ToLower();

            var contestLearner =
                await _contestLearnerRepository.FetchByUserNameAsync(
                    contestLearnerAdd.ContestId,
                    contestLearnerAdd.UserName);

            if (contestLearner != null)
            {
                throw new ArgumentOutOfRangeException(
                    $"The contest learner {contestLearnerAdd.UserName} already exists.");
            }

            contestLearner =
                 _mapper.Map(contestLearnerAdd, new ContestLearner());

            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(
                    contestLearnerAdd.UserName);

            contestLearner =
                _mapper.Map(microsoftProfile, contestLearner);

            contestLearner =
                _contestLearnerStartValueUpdater.Update(
                    contest,
                    contestLearner,
                    microsoftProfile);

            contestLearner =
                _contestLearnerTargetValueUpdater.Update(
                    contest,
                    contestLearner);

            if (contest.Status == ContestStatus.InProgress)
            {
                contestLearner =
                    _contestLearnerCurrentValueUpdater.Update(
                        contest,
                        contestLearner,
                        microsoftProfile);
            }

            contestLearner =
                await _contestLearnerRepository.AddAsync(
                    contestLearner.ContestId,
                    contestLearner);

            var contestLearnerAdded =
                _mapper.Map(contestLearner, new ContestLearnerAdded());

            await _eventPublisher.PublishAsync("contestlearner.added", contestLearnerAdded);

            return contestLearnerAdded;
        }
    }
}