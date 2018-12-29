using System.Threading.Tasks;

namespace Praxeum.WebApi.Helpers
{
    public interface IMicrosoftProfileRepository
    {
        Task<MicrosoftProfile> FetchProfileAsync(
             string userName);
    }
}
