using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.WebApp.Pages.Contests
{
    [Authorize(Roles = "Administrator")]
    public class CopyModel : PageModel
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;
        private readonly IHandler<ContestCopy, ContestCopied> _contestCopier;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        public CopyModel(
            IHandler<ContestFetch, ContestFetched> contestFetcher,
            IHandler<ContestCopy, ContestCopied> contestCopier)
        {
            _contestFetcher = contestFetcher;
            _contestCopier = contestCopier;
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contestCopy =
                new ContestCopy
                {
                    Id = id.Value,
                    Name = this.Contest.Name
                };

            var contestCopied =
                await _contestCopier.ExecuteAsync(
                    contestCopy);

            return RedirectToPage("Details", new { id = contestCopied.Id });
        }
    }
}