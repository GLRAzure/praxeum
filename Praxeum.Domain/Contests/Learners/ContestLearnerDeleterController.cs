using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Contests.Learners
{
    [Route("api/contests/{contestId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class ContestLearnerDeleterController : ControllerBase
    {
        private readonly IHandler<ContestLearnerDelete, ContestLearnerDeleted> _contestLearnerDeleter;

        public ContestLearnerDeleterController(
            IHandler<ContestLearnerDelete, ContestLearnerDeleted> contestLearnerDeleter)
        {
            _contestLearnerDeleter = contestLearnerDeleter;
        }

        [HttpDelete("{learnerId}", Name = "DeleteContestLearner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contest Learners" })]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid contestId,
            [FromRoute] Guid learnerId)
        {
            var contestLearnerDelete =
                new ContestLearnerDelete
                {
                    ContestId = contestId,
                    LearnerId = learnerId
                };

            await _contestLearnerDeleter.ExecuteAsync(contestLearnerDelete);

            return NoContent();
        }
    }
}