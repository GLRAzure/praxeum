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

namespace Praxeum.WebApp.Pages.Learners
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AzureAdB2COptions _azureAdB2COptions;

        public SelectList AvailableLeaderBoards { get; set; }


        [BindProperty]
        public LearnerCreateModel Learner { get; set; }

        public CreateModel(
           IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
        }

        public async Task<IActionResult> OnGet()
        {
            await this.GetAvailableLeaderBoardsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await this.GetAvailableLeaderBoardsAsync();

                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var separators =
                    new[] { Environment.NewLine, ",", ";", "|" };

                var names =
                    this.Learner.Names.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var response =
                    await httpClient.PostAsJsonAsync($"{_azureAdB2COptions.ApiUrl}/learners",
                        new { this.Learner.LeaderBoardId, Names = names });

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                var learner =
                     JsonConvert.DeserializeObject<LearnerCreatedModel>(content);

                return RedirectToPage("./Index");
            }
        }

        private async Task GetAvailableLeaderBoardsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync($"{_azureAdB2COptions.ApiUrl}/leaderboards");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                var leaderBoards =
                     JsonConvert.DeserializeObject<IEnumerable<LeaderBoardIndexModel>>(content);

                leaderBoards =
                    leaderBoards.OrderBy(x => x.Name);

                this.AvailableLeaderBoards = new SelectList(leaderBoards, "Id", "Name");
            }
        }
    }
}