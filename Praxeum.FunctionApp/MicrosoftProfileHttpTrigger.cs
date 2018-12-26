using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class MicrosoftProfileHttpTrigger
    {
        [FunctionName("MicrosoftProfileHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "learners")] HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            if (name == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                name = data?.name;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return req.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Please pass a name on the query string or in the request body");
            }

            var microsoftProfileScraper =
                new MicrosoftProfileScraper();

            var microsoftProfile = 
                microsoftProfileScraper.FetchProfile(name);

            return req.CreateResponse(HttpStatusCode.OK, microsoftProfile);
        }
    }
}
