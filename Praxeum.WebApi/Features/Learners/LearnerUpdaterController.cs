using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerUpdaterController : ControllerBase
    {
        private readonly IHandler<LearnerUpdate, LearnerUpdated> _learnerUpdater;

        public LearnerUpdaterController(
            IHandler<LearnerUpdate, LearnerUpdated> learnerUpdater)
        {
            _learnerUpdater = learnerUpdater;
        }

        [HttpPut("{id}", Name = "UpdateLearner")]
        [ProducesResponseType(200, Type = typeof(LearnerUpdated))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid id,
            [FromBody] LearnerUpdate learnerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            learnerUpdate.Id = id;

            var learnerUpdated =
                await _learnerUpdater.ExecuteAsync(learnerUpdate);

            return Ok(learnerUpdated);
        }
    }
}