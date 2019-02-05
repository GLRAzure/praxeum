using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.WebApp.Areas.Public.Pages.Contests
{
    public class DetailsModel : PageModel
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        public DetailsModel(
            IHandler<ContestFetch, ContestFetched> contestFetcher)
        {
            _contestFetcher = contestFetcher;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.Contest =
                await _contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = id.Value
                    });

            this.Contest.Learners =
                this.Contest.Learners
                    .OrderByDescending(x => x.Rank)
                        .ToList();

            return Page();
        }
    }
}