using System;
using System.Web;

namespace Praxeum.FunctionApp.Helpers
{
    public static class UriExtensions
    {
        public static Uri AddParameter(
            this Uri url, 
            string name, 
            object value)
        {
            var uriBuilder = 
                new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query[name] = value.ToString();

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
