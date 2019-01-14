using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerFetch
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; internal set; }
    }
}