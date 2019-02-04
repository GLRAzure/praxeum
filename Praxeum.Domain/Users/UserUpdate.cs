using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Users
{
    public class UserUpdate 
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}