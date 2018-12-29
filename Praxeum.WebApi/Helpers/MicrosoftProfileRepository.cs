using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Praxeum.WebApi.Helpers
{
    public class MicrosoftProfileRepository : IMicrosoftProfileRepository
    {
        private readonly IOptions<MicrosoftProfileOptions> _microsoftProfileOptions;
        private readonly HttpClient _httpClient;

        public MicrosoftProfileRepository(
            IOptions<MicrosoftProfileOptions> microsoftProfileOptions)
        {
            _microsoftProfileOptions =
                microsoftProfileOptions;

            _httpClient =
                new HttpClient();

            _httpClient.BaseAddress = new Uri(_microsoftProfileOptions.Value.ApiEndpoint);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<MicrosoftProfile> FetchProfileAsync(
            string userName)
        {
            MicrosoftProfile microsoftProfile = null;

            var response =
                await _httpClient.GetAsync($"profiles/microsoft/{userName}");

            if (response.IsSuccessStatusCode)
            {
                microsoftProfile = 
                    await response.Content.ReadAsAsync<MicrosoftProfile>();
            }
   
            return microsoftProfile;
        }
    }
}
