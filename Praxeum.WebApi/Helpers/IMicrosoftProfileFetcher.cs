using System.Threading.Tasks;

namespace Praxeum.WebApi.Helpers
{
    public interface IMicrosoftProfileFetcher
    {
        Task<MicrosoftProfile> FetchProfileAsync(
             string userName);
    }
}
