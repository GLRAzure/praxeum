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
    public class ChallengeLearnerDeleterController : ControllerBase
    {
        private readonly IHandler<ChallengeLearnerDelete, ChallengeLearnerDeleted> _challengeLearnerDeleter;

        public ChallengeLearnerDeleterController(
            IHandler<ChallengeLearnerDelete, ChallengeLearnerDeleted> challengeLearnerDeleter)
        {
            _challengeLearnerDeleter = challengeLearnerDeleter;
        }

        [HttpDelete("{learnerId}", Name = "DeleteChallengeLearner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenge Learners" })]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid challengeId,
            [FromRoute] Guid learnerId)
        {
            var challengeLearnerDelete =
                new ChallengeLearnerDelete
                {
                    ChallengeId = challengeId,
                    LearnerId = learnerId
                };

            await _challengeLearnerDeleter.ExecuteAsync(challengeLearnerDelete);

            return NoContent();
        }
    }
}