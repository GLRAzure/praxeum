using System;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerListUpdate
    {
        public DateTime ExpiresOn { get; set; }

        public LearnerListUpdate()
        {
            this.ExpiresOn =
                DateTime.UtcNow.AddDays(-1);
        }
    }
}