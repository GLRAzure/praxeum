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

            foreach (var name in learnerListAdd.Names)
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