using AutoMapper;
using Praxeum.Data;
using System;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
        private readonly IMapper _mapper;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;

        public ContestLearnerAdder(
            IMapper mapper,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository)
        {
            _mapper =
                mapper;
            _contestLearnerRepository =
                contestLearnerRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
        }

        public async Task<ContestLearnerAdded> ExecuteAsync(
            ContestLearnerAdd contestLearnerAdd)
        {
            contestLearnerAdd.UserName =
                contestLearnerAdd.UserName.ToLower();

            var contestLearner =
                await _contestLearnerRepository.FetchByUserNameAsync(
                    contestLearnerAdd.ContestId,
                    contestLearnerAdd.UserName);

            if (contestLearner != null)
            {
                throw new ArgumentOutOfRangeException($"The contest learner {contestLearnerAdd.UserName} already exists.");
            }

            contestLearner =
                 _mapper.Map(contestLearnerAdd, new ContestLearner());

            try
            {
                var microsoftProfile =
                    await _microsoftProfileRepository.FetchProfileAsync(
                        contestLearnerAdd.UserName);
                contestLearner =
                    _mapper.Map(microsoftProfile, contestLearner);

                contestLearner.OriginalProgressStatus =
                    _mapper.Map(microsoftProfile.ProgressStatus, new ContestLearnerProgressStatus());

                contestLearner.Status = ContestLearnerStatus.Updated;
                contestLearner.StatusMessage = string.Empty;
            }
            catch (Exception ex)
            {
                contestLearner.Status = ContestLearnerStatus.Failed;
                contestLearner.StatusMessage = ex.Message;
            }

            contestLearner =
                await _contestLearnerRepository.AddAsync(
                    contestLearner.ContestId,
                    contestLearner);

            var contestLearnerAdded =
                _mapper.Map(contestLearner, new ContestLearnerAdded());

            return contestLearnerAdded;
        }
    }
}