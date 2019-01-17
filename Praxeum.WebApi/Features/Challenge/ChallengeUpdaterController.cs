using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges
{
    [Route("api/challenges")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeUpdaterController : ControllerBase
    {
        private readonly IHandler<ChallengeUpdate, ChallengeUpdated> _challengeUpdater;

        public ChallengeUpdaterController(
            IHandler<ChallengeUpdate, ChallengeUpdated> challengeUpdater)
        {
            _challengeUpdater = challengeUpdater;
        }

        [HttpPut("{id}", Name = "UpdateChallenge")]
        [ProducesResponseType(200, Type = typeof(ChallengeUpdated))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenges" })]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid id,
            [FromBody] ChallengeUpdate challengeUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            challengeUpdate.Id = id;

            var challengeUpdated =
                await _challengeUpdater.ExecuteAsync(challengeUpdate);

            return Ok(challengeUpdated);
        }
    }
}