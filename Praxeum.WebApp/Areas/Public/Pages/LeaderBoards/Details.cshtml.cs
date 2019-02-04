using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;

namespace Praxeum.WebApp.Areas.Public.Pages.LeaderBoards
{
    public class DetailsModel : PageModel
    {
        private readonly IHandler<LeaderBoardFetch, LeaderBoardFetched> _leaderBoardFetcher;

        [BindProperty]
        public LeaderBoardFetched LeaderBoard { get; set; }

        public DetailsModel(
            IHandler<LeaderBoardFetch, LeaderBoardFetched> leaderBoardFetcher)
        {
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

            this.LeaderBoard.Learners =
                this.LeaderBoard.Learners
                    .OrderByDescending(x => x.Rank)
                        .ToList();

            return Page();
        }
    }
}