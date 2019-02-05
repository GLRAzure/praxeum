using AutoMapper;
using Praxeum.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
        private readonly IContestRepository _contestRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;

        public ContestLearnerAdder(
            IContestRepository contestRepository,
            IMicrosoftProfileRepository microsoftProfileRepository)
        {
            _contestRepository =
                contestRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
        }

        public async Task<ContestLearnerAdded> ExecuteAsync(
            ContestLearnerAdd contestLearnerAdd)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerAdd.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerAdd.ContestId} not found.");
            }

            var contestLearner =
                contest.Learners.SingleOrDefault(
                    x => x.UserName != contestLearnerAdd.UserName);

            if (contestLearner == null)
            {
                var microsoftProfile =
                    await _microsoftProfileRepository.FetchProfileAsync(
                        contestLearnerAdd.UserName);

                if (microsoftProfile == null)
                {
                    throw new NullReferenceException($"Microsoft profile {contestLearnerAdd.UserName} not found.");
                }

                contestLearner =
                    Mapper.Map(microsoftProfile, new ContestLearner());

                contestLearner.OriginalProgressStatus =
                    Mapper.Map(microsoftProfile.ProgressStatus, new ContestLearnerProgressStatus());

                contest.Learners.Add(
                    contestLearner);

                contest =
                    await _contestRepository.UpdateByIdAsync(
                        contest.Id,
                        contest);
            }

            var contestLearnerAdded =
                Mapper.Map(contestLearner, new ContestLearnerAdded());

            contestLearnerAdded.ContestId = 
                contestLearnerAdd.ContestId;

            return contestLearnerAdded;
        }
    }
}