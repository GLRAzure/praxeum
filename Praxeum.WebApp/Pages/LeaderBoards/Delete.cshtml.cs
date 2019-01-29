using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.WebApp.Helpers;
using Praxeum.WebApp.Models;

namespace Praxeum.WebApp.Pages.LeaderBoards
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly AzureAdB2COptions _azureAdB2COptions;

        [BindProperty]
        public LeaderBoardDeleteModel LeaderBoard { get; set; }

        public DeleteModel(
           IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
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
                    await httpClient.GetAsync($"{_azureAdB2COptions.ApiUrl}/leaderboards/{id}");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                this.LeaderBoard =
                     JsonConvert.DeserializeObject<LeaderBoardDeleteModel>(content);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? id)
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.DeleteAsync($"{_azureAdB2COptions.ApiUrl}/leaderboards/{id}");

                response.EnsureSuccessStatusCode();

                return RedirectToPage("./Index");
            }
        }
    }
}