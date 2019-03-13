using FluentAssertions;
using NUnit.Framework;
using Praxeum.Data;
using System.Threading.Tasks;

namespace MeijerDataCollectMVP.FuncAppTests
{
    public class MicrosoftProfileRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            // TODO: Add setup code
        }

        [Test]
        public async Task When_FetchProfile1_ThenResponseIsNotNull()
        {
            var microsoftProfileRepository =
                new MicrosoftProfileRepository();

            var microsoftProfile =
                await microsoftProfileRepository.FetchProfileAsync("mattruma");

            microsoftProfile.Should().NotBeNull();
            microsoftProfile.UserName.Should().NotBeNull();
            microsoftProfile.DisplayName.Should().NotBeNull();
        }

        [Test]
        public async Task When_FetchProfile2_ThenResponseIsNotNull()
        {
            var microsoftProfileRepository =
                new MicrosoftProfileRepository();

            var microsoftProfile =
                await microsoftProfileRepository.FetchProfileAsync("bpd");

            microsoftProfile.Should().NotBeNull();
            microsoftProfile.UserName.Should().NotBeNull();
            microsoftProfile.DisplayName.Should().NotBeNull();
        }
    }
}
