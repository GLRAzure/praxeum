using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerListAdd
    {
        public Guid? LeaderBoardId { get; set; }

        [Required]
        public string[] Names { get; set; }
    }
}