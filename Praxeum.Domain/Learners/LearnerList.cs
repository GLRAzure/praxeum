namespace Praxeum.Domain.Learners
{
    public class LearnerList
    {
        public string Status { get; set; }
        public int? MaximumRecords { get; set; }
        public string OrderBy { get; set; }

        public LearnerList()
        {
            this.OrderBy = "l.displayName";
        }
    }
}