using System;
using System.Linq;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerFetchByUserName
    {
        public string UserName { get; set; }

        public int CacheExpirationInMinutes { get; set; }

        public LearnerFetchByUserName()
        {
            var cacheExpirationInMinutes =
                Environment.GetEnvironmentVariable("CacheExpirationInMinutes");

            if (cacheExpirationInMinutes.All(Char.IsDigit))
            {
                this.CacheExpirationInMinutes =
                    Convert.ToInt32(cacheExpirationInMinutes);
            } else
            {
                this.CacheExpirationInMinutes = 1440; // 24 hours
            }
        }
    }
}