using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetched : ContestLearner
    {
        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}