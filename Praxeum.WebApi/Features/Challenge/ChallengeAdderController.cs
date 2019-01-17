using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges
{
    [Route("api/challenges")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeAdderController : ControllerBase
    {
        private readonly IHandler<ChallengeAdd, ChallengeAdded> _challengeAdder;

        public ChallengeAdderController(
            IHandler<ChallengeAdd, ChallengeAdded> challengeAdder)
        {
            _challengeAdder = challengeAdder;
        }

        [HttpPost(Name = "AddChallenge")]
        [ProducesResponseType(201, Type = typeof(ChallengeAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenges" })]
        public async Task<IActionResult> AddAsync(
            [FromBody] ChallengeAdd challengeAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var challengeAdded =
                await _challengeAdder.ExecuteAsync(challengeAdd);

            return CreatedAtRoute("FetchChallenge", new { id = challengeAdded.Id }, challengeAdded);
        }
    }
}