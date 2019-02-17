using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.Domain.Contests
{
    public class ContestProgressUpdater : IHandler<ContestProgressUpdate, ContestProgressUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestProgressUpdater(
            IMapper mapper,
            IEventPublisher eventPublisher,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _eventPublisher =
                eventPublisher;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestProgressUpdated> ExecuteAsync(
            ContestProgressUpdate contestUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestUpdate.Id);

            if (contest.IsStatus(ContestStatus.InProgress))
            {
                var contestLearners =
                    await _contestLearnerRepository.FetchListAsync(
                        contest.Id);

                foreach (var contestLearner in contestLearners)
                {
                    var contestLearnerProgressUpdate =
                        _mapper.Map(contestLearner, new ContestLearnerProgressUpdate());

                    await _eventPublisher.PublishAsync("contestlearnerprogress.update", contestLearnerProgressUpdate);
                }

                contest.LastProgressUpdateOn =
                    DateTime.UtcNow;
                contest.NextProgressUpdateOn =
                    contest.LastProgressUpdateOn.Value.AddMinutes(contest.ProgressUpdateInterval);

                contest =
                    await _contestRepository.UpdateByIdAsync(
                        contestUpdate.Id,
                        contest);
            }

            var contestProgressUpdated =
                _mapper.Map(contest, new ContestProgressUpdated());

            return contestProgressUpdated;
        }
    }
}