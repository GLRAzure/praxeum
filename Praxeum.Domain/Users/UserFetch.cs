using System;
using System.Linq;
using System.Security.Claims;

namespace Praxeum.Domain.Users
{
    public class UserFetch
    {
        [SwaggerExclude]
        public string Id { get; set; }

        public UserFetch()
        {
        }

        public UserFetch(
            ClaimsPrincipal principal)
        {
            var nameIdentifierClaim =
                principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim == null)
            {
                throw new NullReferenceException($"Missing '{ClaimTypes.NameIdentifier}' claim.");
            }

            this.Id = nameIdentifierClaim.Value;
        }
    }
}