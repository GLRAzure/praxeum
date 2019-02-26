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
    public class EditModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;
        private readonly IHandler<ContestUpdate, ContestUpdated> _contestUpdater;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        public EditModel(
            IMapper mapper,
            IHandler<ContestFetch, ContestFetched> contestFetcher,
            IHandler<ContestUpdate, ContestUpdated> contestUpdater)
        {
            _mapper = mapper;
            _contestFetcher = contestFetcher;
            _contestUpdater = contestUpdater;
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

            var contestUpdate =
                _mapper.Map(
                    this.Contest, new ContestUpdate());
            
            contestUpdate.Id = id.Value;

            var contestUpdated =
                await _contestUpdater.ExecuteAsync(
                    contestUpdate);

            return RedirectToPage("Details", new { id = contestUpdated.Id });
        }
    }
}