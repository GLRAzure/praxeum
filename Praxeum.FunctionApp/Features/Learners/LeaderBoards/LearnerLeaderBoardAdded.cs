using System;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LearnerLeaderBoardAdded
    {
        public Guid LearnerId { get; set; }

        public Guid LeaderBoardId { get; set; }
    }
}