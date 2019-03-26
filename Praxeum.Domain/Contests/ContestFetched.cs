using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;
using System;
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

        public int Growth
        {
            get
            {
                var result = 0;

                if (this.Learners == null) return result;

                foreach (var learner in this.Learners)
                {
                    if (this.IsPointsContest() && learner.PointsGrowthValue.HasValue)
                    {
                        result += learner.PointsGrowthValue.Value;
                    }
                    else if (learner.LevelGrowthValue.HasValue)
                    {
                        result += learner.LevelGrowthValue.Value;
                    }
                }

                return result;
            }
        }

        [Obsolete("Use the Growth property")]
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