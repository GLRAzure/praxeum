using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Areas.Public.Pages.Contests
{
    public class RegisterModel : PageModel
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;
        private readonly IHandler<ContestLearnerAdd, ContestLearnerAdded> _contestLearnerAdder;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        [BindProperty]
        public ContestLearnerAdd Learner { get; set; }

        public RegisterModel(
           IHandler<ContestFetch, ContestFetched> contestFetcher,
           IHandler<ContestLearnerAdd, ContestLearnerAdded> contestLearnerAdder)
        {
            _contestFetcher =
                contestFetcher;
            _contestLearnerAdder =
                contestLearnerAdder;
        }

        public async Task<IActionResult> OnGet(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.GetContestAsync(
                id.Value);

            this.Learner =
                new ContestLearnerAdd
                {
                    ContestId = id.Value
                };

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
                await this.GetContestAsync(
                    id.Value);

                return Page();
            }

            await _contestLearnerAdder.ExecuteAsync(
                this.Learner);

            return RedirectToPage("Details", new { id });
        }

        private async Task GetContestAsync(
            Guid id)
        {
            var contest =
                await _contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = id
                    });

            this.Contest = contest;
        }
    }
}