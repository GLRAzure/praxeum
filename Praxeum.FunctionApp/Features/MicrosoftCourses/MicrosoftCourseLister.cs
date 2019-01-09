using Praxeum.FunctionApp.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftCourses
{
    public class MicrosoftCourseLister : IHandler<MicrosoftCourseList, IEnumerable<MicrosoftCourseListed>>
    {
        public MicrosoftCourseLister()
        {
        }

        public async Task<IEnumerable<MicrosoftCourseListed>> ExecuteAsync(
            MicrosoftCourseList microsoftCourseList)
        {
            var httpClient =
                new HttpClient();

            var requestUri = new Uri("https://docs.microsoft.com/api/contentbrowser/search");

            requestUri = requestUri.AddParameter("environment", "prod");
            requestUri = requestUri.AddParameter("locale", "en-us");
            requestUri = requestUri.AddParameter("filter=((products/any(t: t eq 'azure')))", "prod");
            requestUri = requestUri.AddParameter("orderBy", "title desc");
            requestUri = requestUri.AddParameter("skip", microsoftCourseList.Page);
            requestUri = requestUri.AddParameter("top", microsoftCourseList.PageSize);

            var httpResponseMessage = await httpClient.GetAsync(requestUri);

            var microsoftCourseListed =
                new List<MicrosoftCourseListed>();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonString  = 
                    await httpResponseMessage.Content.ReadAsAsync<dynamic>();

                foreach(var result in jsonString.results)
                {
                     microsoftCourseListed.Add(
                         new MicrosoftCourseListed(result)); 
                }
            }

            return microsoftCourseListed;
        }
    }
}