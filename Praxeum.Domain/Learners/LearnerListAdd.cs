using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Learners
{
    public class LearnerListAdd
    {
        public Guid? LeaderBoardId { get; set; }

        [Required]
        public string[] Names { get; set; }
    }
}