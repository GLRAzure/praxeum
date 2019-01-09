using Praxeum.FunctionApp.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftCourses
{
    public class MicrosoftCourseFetcher : IHandler<MicrosoftCourseFetch, MicrosoftCourseFetched>
    {
        public MicrosoftCourseFetcher()
        {
        }

        public async Task<MicrosoftCourseFetched> ExecuteAsync(
            MicrosoftCourseFetch microsoftCourseFetch)
        {
            var httpClient =
                new HttpClient();

            var requestUri = new Uri("https://docs.microsoft.com/api/contentbrowser/search");

            requestUri = requestUri.AddParameter("environment", "prod");
            requestUri = requestUri.AddParameter("locale", "en-us");
            requestUri = requestUri.AddParameter("filter=((products/any(t: t eq 'azure')))", "prod");
            requestUri = requestUri.AddParameter("skip", 0);
            requestUri = requestUri.AddParameter("top", 1);

            if (!string.IsNullOrWhiteSpace(microsoftCourseFetch.Id))
            {
                requestUri = requestUri.AddParameter("terms", microsoftCourseFetch.Id);
            }

            if (!string.IsNullOrWhiteSpace(microsoftCourseFetch.Name))
            {
                requestUri = requestUri.AddParameter("terms", microsoftCourseFetch.Name);
            }

            var httpResponseMessage = await httpClient.GetAsync(requestUri);

            MicrosoftCourseFetched microsoftCourseFetched = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonString =
                    await httpResponseMessage.Content.ReadAsAsync<dynamic>();

                foreach (var result in jsonString.results)
                {
                   microsoftCourseFetched =
                        new MicrosoftCourseFetched(result);
                }
            }

            return microsoftCourseFetched;
        }
    }
}