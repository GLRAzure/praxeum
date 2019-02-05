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

        public ContestLearnerListAdder(
            IContestRepository contestRepository,
            IEventPublisher eventPublisher)
        {
            _contestRepository =
                contestRepository;
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
                    var contestLearnerAdd =
                        new ContestLearnerAdd
                        {
                            ContestId = contestLearnerListAdd.ContestId,
                            UserName = userName
                        };

                    await _eventPublisher.PublishAsync("contestlearner.add", contestLearnerAdd);
                }
            }

            return contestLearnerListAdded;
        }
    }
}