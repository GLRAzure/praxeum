using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftProfiles
{
    public class MicrosoftProfileFetchHandler
    {
        private readonly IMicrosoftProfileScraper _microsoftProfileScraper;

        public MicrosoftProfileFetchHandler() : this(new MicrosoftProfileScraper())
        {
        }

        public MicrosoftProfileFetchHandler(
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