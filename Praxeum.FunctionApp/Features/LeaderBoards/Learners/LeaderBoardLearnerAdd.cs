using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerAdd
    {
        public Guid Id { get; set; }

        public Guid LeaderBoardId { get; set; }

        [Required]
        public Guid LearnerId { get; set; }


        public LeaderBoardLearnerAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}