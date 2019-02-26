using System;
using System.Linq;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdder : IHandler<ContestLearnerListAdd, ContestLearnerListAdded>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestLearnerListAdder(
            IContestLearnerRepository contestLearnerRepository,
            IEventPublisher eventPublisher)
        {
            _contestLearnerRepository =
                contestLearnerRepository;
            _eventPublisher =
                eventPublisher;
        }

        public async Task<ContestLearnerListAdded> ExecuteAsync(
            ContestLearnerListAdd contestLearnerListAdd)
        {
            var contestLearners =
                await _contestLearnerRepository.FetchListAsync(
                    contestLearnerListAdd.ContestId);

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
                if (contestLearners.All(x => x.UserName != userName))
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

            await _contestRepository.UpdateByIdAsync(
                contestLearnerListAdd.ContestId,
                contest);

            return contestLearnerListAdded;
        }
    }
}