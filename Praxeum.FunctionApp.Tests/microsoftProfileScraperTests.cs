using NUnit.Framework;
using Praxeum.FunctionApp.Helpers;

namespace Tests
{
    public class MicrosoftProfileScraperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenFetchProfile()
        {
            var microsoftProfileScraper =
                new MicrosoftProfileScraper();

            var result =
                microsoftProfileScraper.FetchProfile("mattruma");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.DisplayName);
            Assert.IsNotNull(result.UserName);
            Assert.IsNotNull(result.CreatedOn);
        }
    }
}