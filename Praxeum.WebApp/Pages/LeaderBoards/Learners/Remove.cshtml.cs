using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Praxeum.WebApp.Helpers;
using Praxeum.WebApp.Models;

namespace Praxeum.WebApp.Pages.LeaderBoards.Learners
{
    [Authorize(Roles = "Administrator")]
    public class RemoveModel : PageModel
    {
        private readonly AzureAdB2COptions _azureAdB2COptions;

        [BindProperty]
        public LeaderBoardLearnerRemoveModel Learner { get; set; }

        public RemoveModel(
           IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
        }

        public IActionResult OnGet(
            Guid leaderBoardId,
            Guid learnerId,
            string learnerName)
        {
            this.Learner =
                new LeaderBoardLearnerRemoveModel
                {
                    LeaderBoardId = leaderBoardId,
                    LearnerId = learnerId,
                    LearnerName = learnerName
                };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid leaderBoardId,
            Guid learnerId,
            string learnerName)
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.DeleteAsync($"{_azureAdB2COptions.ApiUrl}/leaderboards/{leaderBoardId}/learners/{learnerId}");

                response.EnsureSuccessStatusCode();

                return RedirectToPage("/LeaderBoards/Details", new { id = leaderBoardId });
            }
        }
    }
}