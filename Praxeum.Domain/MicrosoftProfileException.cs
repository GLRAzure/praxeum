using Praxeum.Data;
using System;

namespace Praxeum.Domain
{
    public class MicrosoftProfileException : Exception
    {
        public ContestLearner ContestLearner { get; private set; }

        public MicrosoftProfileException(
            ContestLearner contestLearner,
            Exception ex) : base(ex.Message, ex)
        {
            this.ContestLearner = contestLearner;
        }
    }
}
