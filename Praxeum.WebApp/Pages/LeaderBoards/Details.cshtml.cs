﻿using System;
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
    [Authorize]
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
            }

            return Page();
        }
    }
}