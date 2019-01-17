using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges
{
    [Route("api/challenges")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeListerController : ControllerBase
    {
        private readonly IHandler<ChallengeList, IEnumerable<ChallengeListed>> _challengeLister;

        public ChallengeListerController(
            IHandler<ChallengeList, IEnumerable<ChallengeListed>> challengeLister)
        {
            _challengeLister = challengeLister;
        }

        [HttpGet(Name = "FetchChallenges")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ChallengeListed>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenges" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var ChallengeListed =
                await _challengeLister.ExecuteAsync(
                    new ChallengeList());

            return Ok(ChallengeListed);
        }
    }
}