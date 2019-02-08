using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetched : ContestLearner
    {
        public int DeltaValue => this.CurrentValue - (this.StartValue + this.TargetValue);

        public int ProgressValue
        {
            get
            {
                var result = this.CurrentValue - this.StartValue;

                if (result > this.TargetValue)
                {
                    result = this.TargetValue;
                }

                return result;
            }
        }

        public int ProgressPercentage
        {
            get
            {
                var result = 0;

                if (this.StartValue != 0 || this.TargetValue != 0)
                {
                    result = (int)(((decimal)this.ProgressValue / (decimal)(this.TargetValue)) * 100);
                }

                return result;
            }
        }

        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}