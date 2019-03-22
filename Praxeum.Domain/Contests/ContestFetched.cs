using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;
using System.Collections.Generic;

namespace Praxeum.Domain.Contests
{
    public class ContestFetched : Contest
    {
        public ICollection<ContestLearnerFetched> Learners { get; set; }

        public ContestFetched()
        {
            this.Learners = new List<ContestLearnerFetched>();
        }

        public int GetTotalLearnerGrowth()
        {
            int result = 0;

            foreach (var learner in this.Learners)
            {
                if (this.IsPointsContest())
                {
                    result += learner.PointsGrowthValue.Value;
                }
                else
                {
                    result += learner.LevelGrowthValue.Value;
                }
            }

            return result;
        }
    }
}