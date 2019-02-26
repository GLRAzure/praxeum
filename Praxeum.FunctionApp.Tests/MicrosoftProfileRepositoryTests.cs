using NUnit.Framework;
using Praxeum.Data;

namespace Tests
{
    public class MicrosoftProfileRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenFetchProfile()
        {
            var microsoftProfileRepository =
                new MicrosoftProfileRepository();

            var result =
                microsoftProfileRepository.FetchProfileAsync("mattruma").Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.DisplayName);
            Assert.IsNotNull(result.UserName);
            Assert.IsNotNull(result.CreatedOn);
        }
    }
}