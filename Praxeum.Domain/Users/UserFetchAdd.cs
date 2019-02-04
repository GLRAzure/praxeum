using System.Security.Claims;

namespace Praxeum.Domain.Users
{
    public class UserFetchAdd : UserAdd
    {
        public UserFetchAdd()
        {
        }

        public UserFetchAdd(
            ClaimsPrincipal principal) : base(principal)
        {
        }
    }
}