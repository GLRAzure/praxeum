using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IHandler<ContestLearnerFetch, ContestLearnerFetched> _contestLearnerFetcher;
        private readonly IHandler<ContestLearnerDelete, ContestLearnerDeleted> _contestLearnerDeleter;

        [BindProperty]
        public ContestLearnerFetched Learner { get; set; }

        public DeleteModel(
           IHandler<ContestLearnerFetch, ContestLearnerFetched> contestLearnerFetcher,
           IHandler<ContestLearnerDelete, ContestLearnerDeleted> contestLearnerDeleter)
        {
            _contestLearnerFetcher =
                contestLearnerFetcher;
            _contestLearnerDeleter =
                contestLearnerDeleter;
        }

        public async Task<IActionResult> OnGet(
            Guid? contestId,
            Guid? id)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            this.Learner =
                await _contestLearnerFetcher.ExecuteAsync(
                    new ContestLearnerFetch
                    {
                        ContestId = contestId.Value,
                        Id = id.Value
                    });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? contestId,
            Guid? id)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            await _contestLearnerDeleter.ExecuteAsync(
                new ContestLearnerDelete
                {
                    ContestId = contestId.Value,
                    Id = id.Value
                });

            return RedirectToPage("/Contests/Details", new { id = contestId });
        }
    }
}