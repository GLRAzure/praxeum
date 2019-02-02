using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdd
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [SwaggerExclude]
        public Guid ContestId { get; set; }

        [Required]
        public Guid LearnerId { get; set; }


        public ContestLearnerAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}