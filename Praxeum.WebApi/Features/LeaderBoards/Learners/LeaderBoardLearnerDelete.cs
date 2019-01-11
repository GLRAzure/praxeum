using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }
    }
}