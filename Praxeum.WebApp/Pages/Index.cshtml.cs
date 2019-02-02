using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;
using Praxeum.Domain.Learners;

namespace Praxeum.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> _leaderBoardLister;
        private readonly IHandler<LearnerList, IEnumerable<LearnerListed>> _learnerLister;

        public IEnumerable<LeaderBoardListed> LeaderBoards { get; private set; }
        public IEnumerable<LearnerListed> Learners { get; private set; }

        public IndexModel(
            IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> leaderBoardLister,
            IHandler<LearnerList, IEnumerable<LearnerListed>> learnerLister)
        {
            _leaderBoardLister =
                leaderBoardLister;
            _learnerLister =
                learnerLister;
        }

        public async Task OnGet()
        {
            this.LeaderBoards =
                await _leaderBoardLister.ExecuteAsync(
                    new LeaderBoardList());

            this.Learners =
                await _learnerLister.ExecuteAsync(
                    new LearnerList
                    {
                        MaximumRecords = 20,
                        OrderBy = "l.rank DESC"
                    });
        }
    }
}
