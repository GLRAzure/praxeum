using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Teamaloo.WebApp.Helpers;
using Teamaloo.WebApp.Models;

namespace Teamaloo.WebApp.Pages.Teams
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly string _apiEndpoint;
        private readonly IConfigurationService _configurationService;

        public TeamModel Team { get; set; }

        public DeleteModel(
            IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            _apiEndpoint = _configurationService.GetValue("Teamaloo:ApiEndpoint");
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
                    await httpClient.GetAsync($"{_apiEndpoint}/teams/{id}");

                response.EnsureSuccessStatusCode();

                var content =
                    await response.Content.ReadAsStringAsync();

                this.Team =
                     JsonConvert.DeserializeObject<TeamModel>(content);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var response =
                    await httpClient.DeleteAsync($"{_apiEndpoint}/teams/{id}");

                response.EnsureSuccessStatusCode();

                return RedirectToPage("./Index");
            }
        }
    }
}