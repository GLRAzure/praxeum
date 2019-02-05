using Praxeum.Data;
using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetched : ContestLearner
    {
        public Guid ContestId { get; set; }

        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})";
    }
}