using Praxeum.Data;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public interface IMicrosoftProfileRepository
    {
        Task<MicrosoftProfile> FetchProfileAsync(
             string userName);
    }
}
