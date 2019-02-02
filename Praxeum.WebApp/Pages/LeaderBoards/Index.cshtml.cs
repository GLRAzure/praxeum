using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;

namespace Praxeum.WebApp.Pages.LeaderBoards
{
    public class IndexModel : PageModel
    {
        private readonly IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> _leaderBoardLister;

        public IEnumerable<LeaderBoardListed> LeaderBoards { get; private set; }

        public IndexModel(
            IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> leaderBoardLister)
        {
            _leaderBoardLister =
                leaderBoardLister;
        }

        public async Task OnGetAsync()
        {
            this.LeaderBoards =
                await _leaderBoardLister.ExecuteAsync(
                    new LeaderBoardList());
        }
    }
}