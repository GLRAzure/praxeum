using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Learners;
using Praxeum.Domain.Learners.LeaderBoards;

namespace Praxeum.WebApp.Pages.LeaderBoards.Learners
{
    [Authorize(Roles = "Administrator")]
    public class RemoveModel : PageModel
    {
        private readonly IHandler<LearnerFetch, LearnerFetched> _learnerFetcher;
        private readonly IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted> _learnerLeaderBoardDeleter;

        [BindProperty]
        public LearnerFetched Learner { get; set; }

        [BindProperty]
        public Guid LeaderBoardId { get; set; }

        public RemoveModel(
           IHandler<LearnerFetch, LearnerFetched> learnerFetcher,
           IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted> learnerLeaderBoardDeleter)
        {
            _learnerFetcher =
                learnerFetcher;
            _learnerLeaderBoardDeleter =
                learnerLeaderBoardDeleter;
        }

        public async Task<IActionResult> OnGet(
            Guid? leaderBoardId,
            Guid? learnerId)
        {
            if (leaderBoardId == null)
            {
                return NotFound();
            }

            if (learnerId == null)
            {
                return NotFound();
            }

            this.LeaderBoardId = 
                leaderBoardId.Value;

            this.Learner =
                await _learnerFetcher.ExecuteAsync(
                    new LearnerFetch
                    {
                        Id = learnerId.Value
                    });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? leaderBoardId,
            Guid? learnerId)
        {
            if (leaderBoardId == null)
            {
                return NotFound();
            }

            if (learnerId == null)
            {
                return NotFound();
            }

            this.LeaderBoardId = 
                leaderBoardId.Value;

            await _learnerLeaderBoardDeleter.ExecuteAsync(
                new LearnerLeaderBoardDelete
                {
                    LeaderBoardId = leaderBoardId.Value,
                    LearnerId = learnerId.Value
                });

            return RedirectToPage("/LeaderBoards/Details", new { id = leaderBoardId });
        }
    }
}