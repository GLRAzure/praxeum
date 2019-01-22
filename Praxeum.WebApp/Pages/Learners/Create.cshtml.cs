using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [BindProperty]
        public LearnerCreateModel Learner { get; set; }

        public CreateModel(
           IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _azureAdB2COptions = azureAdB2COptions.Value;
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

            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.PostAsJsonAsync($"{_azureAdB2COptions.ApiUrl}/learners", this.Learner);

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                var learner =
                     JsonConvert.DeserializeObject<LearnerCreatedModel>(content);

                return RedirectToPage("./Index");
            }
        }
    }
}