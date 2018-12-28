using Praxeum.FunctionApp.Data;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Helpers
{
    public interface IMicrosoftProfileScraper
    {
        Task<MicrosoftProfile> FetchProfileAsync(
             string userPrincipalName);
    }
}
