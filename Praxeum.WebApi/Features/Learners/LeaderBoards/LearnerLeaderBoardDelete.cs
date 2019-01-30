using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners.LeaderBoards
{
    public class LearnerLeaderBoardDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }
    }
}