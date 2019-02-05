using Praxeum.Data;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftProfiles
{
    public class MicrosoftProfileFetcher : IHandler<MicrosoftProfileFetch, MicrosoftProfileFetched>
    {
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;

        public MicrosoftProfileFetcher() : this(new MicrosoftProfileRepository())
        {
        }

        public MicrosoftProfileFetcher(
            IMicrosoftProfileRepository microsoftProfileRepository)
        {
            _microsoftProfileRepository =
                microsoftProfileRepository;
        }

        public async Task<MicrosoftProfileFetched> ExecuteAsync(
            MicrosoftProfileFetch microsoftProfileFetch)
        {
            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(
                    microsoftProfileFetch.UserName);

            if (microsoftProfile == null)
            {
                return null;
            }

            var microsoftProfileFetched =
                new MicrosoftProfileFetched(microsoftProfile);

            return microsoftProfileFetched;
        }
    }
}