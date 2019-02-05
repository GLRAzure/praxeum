using System;
using System.Linq;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdder : IHandler<ContestLearnerListAdd, ContestLearnerListAdded>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestLearnerListAdder(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository,
            IEventPublisher eventPublisher)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
            _eventPublisher =
                eventPublisher;
        }

        public async Task<ContestLearnerListAdded> ExecuteAsync(
            ContestLearnerListAdd contestLearnerListAdd)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerListAdd.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException("Contest does not exist.");
            }

            var contestLearnerListAdded =
                new ContestLearnerListAdded
                {
                    ContestId = contestLearnerListAdd.ContestId
                };

            var separators =
                new[] { Environment.NewLine, ",", ";", "|" };

            var userNames = contestLearnerListAdd.UserNames
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var userName in userNames)
            {
                if (contest.Learners.All(x => x.UserName != userName))
                {
                    contest.Learners.Add(
                        new ContestLearner
                        {
                            UserName = userName,
                            Status = ContestLearnerStatus.New,
                            StatusMessage = string.Empty
                        });
                }
            }

            contest = 
                await _contestRepository.UpdateByIdAsync(
                    contestLearnerListAdd.ContestId,
                    contest);

            foreach (var learner in contest.Learners)
            {
                await _eventPublisher.PublishAsync("contestlearner.add", learner);
            }

            return contestLearnerListAdded;
        }
    }
}