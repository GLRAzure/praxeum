using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerAdd
    {
        public Guid Id { get; set; }

        public Guid? LeaderBoardId { get; set; }

        [Required]
        public string Name { get; set; }


        public LearnerAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}