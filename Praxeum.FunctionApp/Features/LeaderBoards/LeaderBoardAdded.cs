using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardAdded : LeaderBoard
    {
        public LeaderBoardAdded(
            LeaderBoard leaderBoard)
        {
            this.Id = leaderBoard.Id;
            this.Name = leaderBoard.Name;
            this.Description = leaderBoard.Description;
            this.IsActive = leaderBoard.IsActive;
            this.Learners = leaderBoard.Learners;
            this.CreatedOn = leaderBoard.CreatedOn;
        }
    }
}