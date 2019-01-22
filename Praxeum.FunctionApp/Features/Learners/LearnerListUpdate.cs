using System;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerListUpdate
    {
        public DateTime LastModifiedOn { get; set; }

        public LearnerListUpdate()
        {
            this.LastModifiedOn =
                DateTime.UtcNow.AddMinutes(-60);
        }
    }
}