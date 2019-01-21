using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Praxeum.WebApp.Helpers;

namespace Praxeum.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private AzureAdB2COptions _azureAdB2COptions;

        public AccountController(
            IOptions<AzureAdB2COptions> azureAdB2COptions)

        {
            _azureAdB2COptions = azureAdB2COptions.Value;
        }
        
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