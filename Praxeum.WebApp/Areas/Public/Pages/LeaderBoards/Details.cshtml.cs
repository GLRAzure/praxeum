using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.WebApp.Helpers;
using Praxeum.WebApp.Models;

namespace Praxeum.WebApp.Areas.Public.Pages.LeaderBoards
{
    public class DetailsModel : PageModel
    {
        private readonly IOptions<AzureAdB2COptions> _azureAdB2COptions;

        [BindProperty]
        public LeaderBoardDetailsModel LeaderBoard { get; set; }

        public DetailsModel(
            IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.GetAsync($"{_azureAdB2COptions.Value.ApiUrl}/leaderboards/{id}");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                this.LeaderBoard =
                     JsonConvert.DeserializeObject<LeaderBoardDetailsModel>(content);

                this.LeaderBoard.Learners =
                    this.LeaderBoard.Learners
                        .OrderByDescending(x => x.Rank)                            .ThenByDescending(x => x.ProgressStatus.CurrentLevelPointsEarned)
                            .ToList();
            }

            return Page();
        }
    }
}