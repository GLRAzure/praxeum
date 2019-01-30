namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerCountUpdated
    {
        public int NumberOfLeaderBoardsUpdated { get; set; }

        public LeaderBoardLearnerCountUpdated()
        {
            this.NumberOfLeaderBoardsUpdated = 0;
        }
    }
}