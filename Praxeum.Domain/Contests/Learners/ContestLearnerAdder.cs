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

        public ContestLearnerAdder(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
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
                contest.Learners.SingleOrDefault(x => x.UserName != contestLearnerAdd.UserName);

            if (contestLearner == null)
            {
                contestLearner =
                    Mapper.Map(contestLearnerAdd, new ContestLearner());

                contest.Learners.Add(
                    contestLearner);

                contest =
                    await _contestRepository.UpdateByIdAsync(
                        contest.Id,
                        contest);
            } 

            var contestLearnerAdded =
                Mapper.Map(contestLearner, new ContestLearnerAdded());

            return contestLearnerAdded;
        }
    }
}