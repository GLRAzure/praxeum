using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.WebApp.Helpers;
using Praxeum.WebApp.Models;

namespace Praxeum.WebApp.Pages.LeaderBoards.Learners
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModel
    {
        private readonly AzureAdB2COptions _azureAdB2COptions;

        public SelectList AvailableLearners { get; set; }

        [BindProperty]
        public LeaderBoardLearnerAddModel Learner { get; set; }

        public AddModel(
           IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
        }

        public async Task<IActionResult> OnGet(
            Guid leaderBoardId)
        {
            this.Learner =
                new LeaderBoardLearnerAddModel
                {
                    LeaderBoardId = leaderBoardId
                };

            await this.GetAvailableLearnersAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid leaderBoardId)
        {
            if (!ModelState.IsValid)
            {
                await this.GetAvailableLearnersAsync();

                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.PostAsJsonAsync($"{_azureAdB2COptions.ApiUrl}/leaderboards/{leaderBoardId}/learners", this.Learner);

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                var learner =
                     JsonConvert.DeserializeObject<LeaderBoardCreatedModel>(content);

                return RedirectToPage("/LeaderBoards/Details", new { id = leaderBoardId });
            }
        }

        private async Task GetAvailableLearnersAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync($"{_azureAdB2COptions.ApiUrl}/learners");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                var learners =
                     JsonConvert.DeserializeObject<IEnumerable<LearnerIndexModel>>(content);

                learners =
                    learners.OrderBy(x => x.DisplayName);

                this.AvailableLearners = new SelectList(learners, "Id", "DisplayNameAndUserName");
            }
        }
    }
}