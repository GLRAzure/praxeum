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
    public class EditModel : PageModel
    {
        private readonly IHandler<LeaderBoardFetch, LeaderBoardFetched> _leaderBoardFetcher;
        private readonly IHandler<LeaderBoardUpdate, LeaderBoardUpdated> _leaderBoardUpdater;

        [BindProperty]
        public LeaderBoardFetched LeaderBoard { get; set; }

        public EditModel(
           IHandler<LeaderBoardFetch, LeaderBoardFetched> leaderBoardFetcher,
           IHandler<LeaderBoardUpdate, LeaderBoardUpdated> leaderBoardUpdater)
        {
            _leaderBoardFetcher = leaderBoardFetcher;
            _leaderBoardUpdater = leaderBoardUpdater;
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
            Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var leaderBoardUpdate =
                new LeaderBoardUpdate();

            leaderBoardUpdate.Id = id;
            leaderBoardUpdate.Name  = this.LeaderBoard.Name;
            leaderBoardUpdate.Description = this.LeaderBoard.Description;

            var leaderBoardUpdated =
                await _leaderBoardUpdater.ExecuteAsync(
                    leaderBoardUpdate);

            return RedirectToPage("Details", new { id = leaderBoardUpdated.Id });
        }
    }
}