using AutoMapper;
using Praxeum.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;

        public ContestLearnerAdder(
            IMapper mapper,
            IContestRepository contestRepository,
            IMicrosoftProfileRepository microsoftProfileRepository)
        {
            _mapper =
                mapper;
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

            if (contestLearner != null)
            {
                throw new ArgumentOutOfRangeException($"The user {contestLearnerAdd.UserName} already exists.");
            }

            contestLearner =
                 _mapper.Map(contestLearnerAdd, new ContestLearner());

            contest.Learners.Add(
                contestLearner);

            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(
                    contestLearnerAdd.UserName);

            if (microsoftProfile == null)
            {
                contestLearner.Status = ContestLearnerStatus.Failed;
                contestLearner.Status = $"Microsoft profile for {contestLearnerAdd.UserName} not found.";
            }
            else
            {
                contestLearner =
                    _mapper.Map(microsoftProfile, contestLearner);

                contestLearner.OriginalProgressStatus =
                    _mapper.Map(microsoftProfile.ProgressStatus, new ContestLearnerProgressStatus());

                contestLearner.Status = ContestLearnerStatus.Updated;
                contestLearner.StatusMessage = string.Empty;
            }

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contest.Id,
                    contest);

            var contestLearnerAdded =
                _mapper.Map(contestLearner, new ContestLearnerAdded());

            contestLearnerAdded.ContestId =
                contestLearnerAdd.ContestId;

            return contestLearnerAdded;
        }
    }
}