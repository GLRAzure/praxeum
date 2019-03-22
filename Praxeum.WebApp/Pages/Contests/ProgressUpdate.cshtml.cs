using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.WebApp.Pages.Contests
{
    [Authorize(Roles = "Administrator")]
    public class ProgressUpdateModel : PageModel
    {
        private readonly IHandler<ContestProgressUpdate, ContestProgressUpdated> _contestProgressUpdater;
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        public ProgressUpdateModel(
           IHandler<ContestProgressUpdate, ContestProgressUpdated> contestProgressUpdater,
           IHandler<ContestFetch, ContestFetched> contestFetcher)
        {
            _contestProgressUpdater = contestProgressUpdater;
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _contestProgressUpdater.ExecuteAsync(
                 new ContestProgressUpdate
                 {
                     Id = id.Value
                 });

            return RedirectToPage("Details", new { id });
        }
    }
}