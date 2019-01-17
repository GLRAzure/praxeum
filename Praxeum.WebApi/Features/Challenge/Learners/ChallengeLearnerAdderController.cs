using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    [Route("api/challenges/{challengeId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeLearnerAdderController : ControllerBase
    {
        private readonly IHandler<ChallengeLearnerAdd, ChallengeLearnerAdded> _challengeLearnerAdder;

        public ChallengeLearnerAdderController(
            IHandler<ChallengeLearnerAdd, ChallengeLearnerAdded> challengeLearnerAdder)
        {
            _challengeLearnerAdder = challengeLearnerAdder;
        }

        [HttpPost(Name = "AddChallengeLearner")]
        [ProducesResponseType(201, Type = typeof(ChallengeLearnerAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenge Learners" })]
        public async Task<IActionResult> AddAsync(
            [FromRoute] Guid challengeId,
            [FromBody] ChallengeLearnerAdd challengeLearnerAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            challengeLearnerAdd.ChallengeId = 
                challengeId;

            var challengeLearnerAdded =
                await _challengeLearnerAdder.ExecuteAsync(challengeLearnerAdd);

            return CreatedAtRoute("FetchChallengeLearner", new { learnerId = challengeLearnerAdded.Id }, challengeLearnerAdded);
        }
    }
}