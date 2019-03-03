using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestCopier : IHandler<ContestCopy, ContestCopied>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestCopier(
            IMapper mapper,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestCopied> ExecuteAsync(
            ContestCopy contestCopy)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestCopy.Id);

            contest.Id = Guid.NewGuid();
            contest.Name = contestCopy.Name;
            contest.Status = ContestStatus.InProgress;
            contest.StartDate = null;
            contest.EndDate = null;
            contest.CreatedOn = DateTime.UtcNow;

            contest =
                await _contestRepository.AddAsync(
                    contest);

            var contestLearners =
                await _contestLearnerRepository.FetchListAsync(
                    contestCopy.Id);

            foreach (var contestLearner in contestLearners)
            {
                contestLearner.Id = Guid.NewGuid();
                contestLearner.ContestId = contest.Id;

                await _contestLearnerRepository.AddAsync(
                    contest.Id,
                    contestLearner);
            }

            var contestCopied =
                _mapper.Map(contest, new ContestCopied());

            return contestCopied;
        }
    }
}