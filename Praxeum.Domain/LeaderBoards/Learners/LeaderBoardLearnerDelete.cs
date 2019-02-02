using System;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    public class LeaderBoardLearnerDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }
    }
}