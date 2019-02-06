using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerUpdate
    {
        [SwaggerExclude]
        public Guid ContestId { get; set; }

        [SwaggerExclude]
        public Guid Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Start Value")]
        public int StartValue { get; set; }
    }
}