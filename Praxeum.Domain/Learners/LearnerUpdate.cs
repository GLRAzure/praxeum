using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Learners
{
    public class LearnerUpdate 
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}