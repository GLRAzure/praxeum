using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Contests.Learners
{
    [Route("api/contests/{contestId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class ContestLearnerAdderController : ControllerBase
    {
        private readonly IHandler<ContestLearnerAdd, ContestLearnerAdded> _contestLearnerAdder;

        public ContestLearnerAdderController(
            IHandler<ContestLearnerAdd, ContestLearnerAdded> contestLearnerAdder)
        {
            _contestLearnerAdder = contestLearnerAdder;
        }

        [HttpPost(Name = "AddContestLearner")]
        [ProducesResponseType(201, Type = typeof(ContestLearnerAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contest Learners" })]
        public async Task<IActionResult> AddAsync(
            [FromRoute] Guid contestId,
            [FromBody] ContestLearnerAdd contestLearnerAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contestLearnerAdd.ContestId = 
                contestId;

            var contestLearnerAdded =
                await _contestLearnerAdder.ExecuteAsync(contestLearnerAdd);

            return CreatedAtRoute("FetchContestLearner", new { learnerId = contestLearnerAdded.Id }, contestLearnerAdded);
        }
    }
}