using Humanizer;
using Praxeum.Data;
using System;

namespace Praxeum.Domain.Learners
{
    public class LearnerFetched : Learner
    {
        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})"; 

        // https://github.com/Humanizr/Humanizer#humanize-datetime
        public string LastModifiedOnHumanized => this.LastModifiedOn.Humanize();

        public bool IsExpired => this.LastModifiedOn == null || this.LastModifiedOn <= DateTime.UtcNow;
    }
}