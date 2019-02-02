using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;

namespace Praxeum.WebApp.Pages.LeaderBoards
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IHandler<LeaderBoardAdd, LeaderBoardAdded> _leaderBoardAdder;

        [BindProperty]
        public LeaderBoardAdd LeaderBoard { get; set; }

        public CreateModel(
           IHandler<LeaderBoardAdd, LeaderBoardAdded> leaderBoardAdder)
        {
            _leaderBoardAdder = leaderBoardAdder;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var leaderBoardAdded =
                await _leaderBoardAdder.ExecuteAsync(this.LeaderBoard);

            return RedirectToPage("Details", new { id = leaderBoardAdded.Id });
        }
    }
}