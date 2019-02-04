using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetched : ContestLearner
    {
        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}