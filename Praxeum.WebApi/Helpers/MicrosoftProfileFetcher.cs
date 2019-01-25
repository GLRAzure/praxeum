using Microsoft.Extensions.Options;
using Praxeum.Data;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Praxeum.WebApi.Helpers
{
    public class MicrosoftProfileFetcher : IMicrosoftProfileFetcher
    {
        private readonly IOptions<MicrosoftProfileFetcherOptions> _microsoftProfileFetcherOptions;
        private readonly HttpClient _httpClient;

        public MicrosoftProfileFetcher(
            IOptions<MicrosoftProfileFetcherOptions> microsoftProfileFetcherOptions)
        {
            _microsoftProfileFetcherOptions =
                microsoftProfileFetcherOptions;

            _httpClient =
                new HttpClient
                {
                    BaseAddress = new Uri(
                        _microsoftProfileFetcherOptions.Value.ApiEndpoint)
                };

            _httpClient.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", 
                _microsoftProfileFetcherOptions.Value.ApiKey);
        }

        public async Task<MicrosoftProfile> FetchProfileAsync(
            string userName)
        {
            MicrosoftProfile microsoftProfile = null;

            var response =
                await _httpClient.GetAsync($"microsoft/profiles/{userName}");

            if (response.IsSuccessStatusCode)
            {
                microsoftProfile =
                    await response.Content.ReadAsAsync<MicrosoftProfile>();
            }

            return microsoftProfile;
        }
    }
}
