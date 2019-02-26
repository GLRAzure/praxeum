using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Users
{
    public class UserUpdate 
    {
        [SwaggerExclude]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Roles { get; set; }
    }
}