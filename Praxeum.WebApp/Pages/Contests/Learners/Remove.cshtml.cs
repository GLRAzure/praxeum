using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;
using Praxeum.Domain.Learners;
using Praxeum.Domain.Learners.LeaderBoards;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    [Authorize(Roles = "Administrator")]
    public class RemoveModel : PageModel
    {
        private readonly IHandler<LearnerFetch, LearnerFetched> _learnerFetcher;
        private readonly IHandler<ContestLearnerDelete, ContestLearnerDeleted> _contestLearnerDeleter;

        [BindProperty]
        public LearnerFetched Learner { get; set; }

        [BindProperty]
        public Guid ContestId { get; set; }

        public RemoveModel(
           IHandler<LearnerFetch, LearnerFetched> learnerFetcher,
           IHandler<ContestLearnerDelete, ContestLearnerDeleted> contestLearnerDeleter)
        {
            _learnerFetcher =
                learnerFetcher;
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

            this.ContestId = 
                contestId.Value;

            this.Learner =
                await _learnerFetcher.ExecuteAsync(
                    new LearnerFetch
                    {
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

            this.ContestId = 
                contestId.Value;

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