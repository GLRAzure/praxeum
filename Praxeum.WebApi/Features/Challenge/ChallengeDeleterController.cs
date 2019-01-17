using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges
{
    [Route("api/challenges")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeDeleterController : ControllerBase
    {
        private readonly IHandler<ChallengeDelete, ChallengeDeleted> _challengeDeleter;

        public ChallengeDeleterController(
            IHandler<ChallengeDelete, ChallengeDeleted> challengeDeleter)
        {
            _challengeDeleter = challengeDeleter;
        }

        [HttpDelete("{id}", Name = "DeleteChallenge")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenges" })]
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] Guid id)
        {
            var challengeDeleted =
                await _challengeDeleter.ExecuteAsync(
                    new ChallengeDelete
                    {
                        Id = id
                    });

            return NoContent();
        }
    }
}