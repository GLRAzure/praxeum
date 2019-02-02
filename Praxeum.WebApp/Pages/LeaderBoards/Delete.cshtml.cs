using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;

namespace Praxeum.WebApp.Pages.LeaderBoards
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly IHandler<LeaderBoardDelete, LeaderBoardDeleted> _leaderBoardDeleter;
        private readonly IHandler<LeaderBoardFetch, LeaderBoardFetched> _leaderBoardFetcher;

        [BindProperty]
        public LeaderBoardFetched LeaderBoard { get; set; }

        public DeleteModel(
           IHandler<LeaderBoardDelete, LeaderBoardDeleted> leaderBoardDeleter,
           IHandler<LeaderBoardFetch, LeaderBoardFetched> leaderBoardFetcher)
        {
            _leaderBoardDeleter = leaderBoardDeleter;
            _leaderBoardFetcher = leaderBoardFetcher;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.LeaderBoard =
                await _leaderBoardFetcher.ExecuteAsync(
                    new LeaderBoardFetch
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

            await _leaderBoardDeleter.ExecuteAsync(
                 new LeaderBoardDelete
                 {
                     Id = id.Value
                 });

            return RedirectToPage("./Index");
        }
    }
}