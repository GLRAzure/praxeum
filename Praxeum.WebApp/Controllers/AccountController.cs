using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Praxeum.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public async Task SignIn(
            string returnUri = "/")
        {
            await HttpContext.ChallengeAsync("AzureADB2C", new AuthenticationProperties { RedirectUri = returnUri });
        }

        [Authorize]
        public async Task SignOut()
        {
            await HttpContext.SignOutAsync("AzureADB2C", new AuthenticationProperties
            {
                RedirectUri = "/"
            });

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}