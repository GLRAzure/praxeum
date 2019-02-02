using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Domain.Learners
{
    public class LearnerListAdder : IHandler<LearnerListAdd, LearnerListAdded>
    {
        private readonly IHandler<LearnerAdd, LearnerAdded> _learnerAdder;

        public LearnerListAdder(
            IHandler<LearnerAdd, LearnerAdded> learnerAdder)
        {
            _learnerAdder =
                learnerAdder;
        }

        public async Task<LearnerListAdded> ExecuteAsync(
            LearnerListAdd learnerListAdd)
        {
            var learnerListAdded =
                new LearnerListAdded
                {
                    LeaderBoardId = learnerListAdd.LeaderBoardId
                };

            var learners =
                new List<LearnerAdded>();

            var separators =
                new[] { Environment.NewLine, ",", ";", "|" };

            var names = learnerListAdd.Names
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var name in names)
            {
                var learnerAdded =
                    await _learnerAdder.ExecuteAsync(
                        new LearnerAdd
                        {
                            LeaderBoardId = learnerListAdd.LeaderBoardId,
                            Name = name
                        });

                learners.Add(
                    learnerAdded);
            }

            learnerListAdded.Learners =
                learners;

            return learnerListAdded;
        }
    }
}