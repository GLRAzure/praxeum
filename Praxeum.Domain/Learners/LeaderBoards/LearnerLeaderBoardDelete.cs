using System;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    public class LearnerLeaderBoardDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }
    }
}