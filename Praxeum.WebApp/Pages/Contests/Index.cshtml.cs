using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.WebApp.Pages.Contests
{
   [Authorize(Roles = "Administrator")]
   public class IndexModel : PageModel
    {
        private readonly IHandler<ContestList, IEnumerable<ContestListed>> _contestLister;

        public IEnumerable<ContestListed> Contests { get; private set; }

        public IndexModel(
            IHandler<ContestList, IEnumerable<ContestListed>> contestLister)
        {
            _contestLister =
                contestLister;
        }

        public async Task OnGetAsync()
        {
            this.Contests =
                await _contestLister.ExecuteAsync(
                    new ContestList());
        }
    }
}