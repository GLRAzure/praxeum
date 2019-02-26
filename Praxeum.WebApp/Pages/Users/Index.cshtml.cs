using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Users;

namespace Praxeum.WebApp.Pages.Users
{
   [Authorize(Roles = "Administrator")]
   public class IndexModel : PageModel
    {
        private readonly IHandler<UserList, IEnumerable<UserListed>> _userLister;

        public IEnumerable<UserListed> UserProfiles { get; private set; }

        public IndexModel(
            IHandler<UserList, IEnumerable<UserListed>> userLister)
        {
            _userLister =
                userLister;
        }

        public async Task OnGetAsync()
        {
            this.UserProfiles =
                await _userLister.ExecuteAsync(
                    new UserList());
        }
    }
}