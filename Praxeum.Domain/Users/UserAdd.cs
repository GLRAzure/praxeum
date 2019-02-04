using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Praxeum.Domain.Users
{
    public class UserAdd
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Roles { get; set; }

        public UserAdd()
        {
        }

        public UserAdd(
            ClaimsPrincipal principal)
        {
            var nameIdentifierClaim =
                principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim == null)
            {
                throw new NullReferenceException($"Missing '{ClaimTypes.NameIdentifier}' claim.");
            }

            this.Id = nameIdentifierClaim.Value;

            var nameClaim =
                principal.Claims.SingleOrDefault(x => x.Type == "name");

            if (nameClaim == null)
            {
                throw new NullReferenceException($"Missing 'name' claim.");
            }

            this.Name = nameClaim.Value;

            var emailsClaim =
                principal.Claims.SingleOrDefault(x => x.Type == "emails");

            if (emailsClaim == null)
            {
                throw new NullReferenceException($"Missing 'emails' claim.");
            }

            this.Email = emailsClaim.Value;

            this.Roles = "Learner";
        }
    }
}