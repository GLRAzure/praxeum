using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListed : ContestLearner
    {
        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}