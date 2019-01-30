using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LearnerLeaderBoardAdd
    {
        [Required]
        public Guid LearnerId { get; set; }

        [Required]
        public Guid LeaderBoardId { get; set; }
    }
}