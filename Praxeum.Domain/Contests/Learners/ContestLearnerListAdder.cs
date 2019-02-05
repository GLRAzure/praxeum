using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdder : IHandler<ContestLearnerListAdd, ContestLearnerListAdded>
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestLearnerListAdder(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
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

            var names = contestLearnerListAdd.UserNames
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var learners =
                await _learnerRepository.FetchListAsync(names);

            foreach (var learner in learners)
            {
                if (contest.Learners.All(x => x.UserName != learner.UserName))
                {
                    contest.Learners.Add(
                        new ContestLearner
                        {
                            UserName = learner.UserName
                        });
                }
            }

            await _contestRepository.UpdateByIdAsync(
                contestLearnerListAdd.ContestId,
                contest);

            return contestLearnerListAdded;
        }
    }
}