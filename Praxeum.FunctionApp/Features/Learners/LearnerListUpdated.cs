namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerListUpdated
    {
        public int NumberLearnersUpdated { get; set; }

        public LearnerListUpdated()
        {
            this.NumberLearnersUpdated = 0;
        }
    }
}