using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    public class LeaderBoardLearnerAdd
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [SwaggerExclude]
        public Guid LeaderBoardId { get; set; }

        [Required]
        public Guid LearnerId { get; set; }


        public LeaderBoardLearnerAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}