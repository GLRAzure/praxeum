using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardFetchedList : LeaderBoard
    {
        public LeaderBoardFetchedList(
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