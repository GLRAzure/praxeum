using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Praxeum.Data
{
    public class MicrosoftProfileRepository : IMicrosoftProfileRepository
    {
        public async Task<MicrosoftProfile> FetchProfileAsync(
             string userName)
        {
            MicrosoftProfile result;

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;

                response =
                    await httpClient.GetAsync($"https://docs.microsoft.com/api/profiles/{userName}");
                response.EnsureSuccessStatusCode();

                string content;

                content =
                    await response.Content.ReadAsStringAsync();

                result =
                    JsonConvert.DeserializeObject<MicrosoftProfile>(content);

                response =
                    await httpClient.GetAsync($"https://docs.microsoft.com/api/gamestatus/{result.Id}");
                response.EnsureSuccessStatusCode();

                content =
                    await response.Content.ReadAsStringAsync();

                result.GameStatus =
                    JsonConvert.DeserializeObject<MicrosoftProfileGameStatus>(content);
            }

            return result;
        }
    }
}
