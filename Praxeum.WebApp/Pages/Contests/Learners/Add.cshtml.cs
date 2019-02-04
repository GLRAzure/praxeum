using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IHandler<ContestList, IEnumerable<ContestListed>> _contestLister;
        private readonly IHandler<ContestLearnerListAdd, ContestLearnerListAdded> _contestLearnerListAdder;

        public SelectList AvailableContests { get; set; }


        [BindProperty]
        public ContestLearnerListAdd Learner { get; set; }

        public CreateModel(
           IHandler<ContestList, IEnumerable<ContestListed>> contestLister,
           IHandler<ContestLearnerListAdd, ContestLearnerListAdded> contestLearnerListAdder)
        {
            _contestLister =
                contestLister;
            _contestLearnerListAdder =
                contestLearnerListAdder;
        }

        public async Task<IActionResult> OnGet(
            Guid? contestId)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            await this.GetAvailableContestsAsync();

            this.Learner =
                new ContestLearnerListAdd
                {
                    ContestId = contestId.Value
                };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? contestId)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await this.GetAvailableContestsAsync();

                return Page();
            }

            await _contestLearnerListAdder.ExecuteAsync(this.Learner);

            return RedirectToPage("/Contests/Details", new { id = this.Learner.ContestId });
        }

        private async Task GetAvailableContestsAsync()
        {
            var contests =
                await _contestLister.ExecuteAsync(
                    new ContestList());

            contests =
                contests.OrderBy(x => x.Name);

            this.AvailableContests = new SelectList(contests, "Id", "Name");
        }
    }
}