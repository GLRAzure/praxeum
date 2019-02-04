using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdd
    {
        [SwaggerExclude]
        public Guid ContestId { get; set; }

        [Required]
        public string Names { get; set; }
    }
}