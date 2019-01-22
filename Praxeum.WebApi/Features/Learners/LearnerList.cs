namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerList
    {
        public int? MaximumRecords { get; set; }
        public string OrderBy { get; set; }

        public LearnerList()
        {
            this.OrderBy = "l.displayName";
        }
    }
}