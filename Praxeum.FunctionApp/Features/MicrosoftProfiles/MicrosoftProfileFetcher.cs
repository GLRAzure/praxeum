using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftProfiles
{
    public class MicrosoftProfileFetcher : IHandler<MicrosoftProfileFetch, MicrosoftProfileFetched>
    {
        private readonly IMicrosoftProfileScraper _microsoftProfileScraper;

        public MicrosoftProfileFetcher() : this(new MicrosoftProfileScraper())
        {
        }

        public MicrosoftProfileFetcher(
            IMicrosoftProfileScraper microsoftProfileScraper)
        {
            _microsoftProfileScraper =
                microsoftProfileScraper;
        }

        public async Task<MicrosoftProfileFetched> ExecuteAsync(
            MicrosoftProfileFetch microsoftProfileFetch)
        {
            var microsoftProfile =
                await _microsoftProfileScraper.FetchProfileAsync(
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