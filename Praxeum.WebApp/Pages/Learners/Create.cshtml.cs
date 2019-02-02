using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Praxeum.Domain;
using Praxeum.Domain.LeaderBoards;
using Praxeum.Domain.Learners;

namespace Praxeum.WebApp.Pages.Learners
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> _leaderBoardLister;
        private readonly IHandler<LearnerListAdd, LearnerListAdded> _learnerListAdder;

        public SelectList AvailableLeaderBoards { get; set; }


        [BindProperty]
        public LearnerListAdd Learner { get; set; }

        public CreateModel(
           IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> leaderBoardLister,
           IHandler<LearnerListAdd, LearnerListAdded> learnerListAdder)
        {
            _leaderBoardLister =
                leaderBoardLister;
            _learnerListAdder =
                learnerListAdder;
        }

        public async Task<IActionResult> OnGet(
            Guid? leaderBoardId)
        {
            await this.GetAvailableLeaderBoardsAsync();

            this.Learner = 
                new LearnerListAdd
                {
                    LeaderBoardId = leaderBoardId
                };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? leaderBoardId)
        {
            if (!ModelState.IsValid)
            {
                await this.GetAvailableLeaderBoardsAsync();

                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var learnerListAdded =
                    _learnerListAdder.ExecuteAsync(this.Learner);

                if (this.Learner.LeaderBoardId.HasValue)
                {
                    return RedirectToPage("/LeaderBoards/Details", new { id = this.Learner.LeaderBoardId.Value });
                }

                return RedirectToPage("./Index");
            }
        }

        private async Task GetAvailableLeaderBoardsAsync()
        {
            var leaderBoards =
                await _leaderBoardLister.ExecuteAsync(
                    new LeaderBoardList());

            leaderBoards =
                leaderBoards.OrderBy(x => x.Name);

            this.AvailableLeaderBoards = new SelectList(leaderBoards, "Id", "Name");
        }
    }
}