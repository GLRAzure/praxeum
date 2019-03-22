using Praxeum.Data;
using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetched : ContestLearner
    {
        public int? LevelGrowthValue
        {
            get
            {
                int? result = null;

                if (this.Level.HasValue
                    && this.StartValue.HasValue)
                {
                    result = this.Level.Value - this.StartValue.Value;
                }

                return result;
            }
        }

        public int? PointsGrowthValue
        {
            get
            {
                int? result = null;

                if (this.Points.HasValue
                    && this.StartValue.HasValue)
                {
                    result = this.Points.Value - this.StartValue.Value;
                }

                return result;
            }
        }

        public int? ProgressValue
        {
            get
            {
                int? result = null;

                if (this.CurrentValue.HasValue
                    && this.TargetValue.HasValue)
                {
                    result = this.CurrentValue.Value - this.TargetValue.Value;
                }

                return result;
            }
        }

        public int? ProgressPercentage
        {
            get
            {
                int? result = null;

                if (this.CurrentValue.HasValue
                    && this.TargetValue.HasValue)
                {
                    result = Convert.ToInt32(
                        ((decimal)this.CurrentValue.Value / (decimal)this.TargetValue.Value) * 100);
                }

                if (result > 100)
                {
                    result = 100;
                }

                return result;
            }
        }

        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}