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
    public class LearnerUpdateByIdController : ControllerBase
    {
        private readonly ILearnerHandler<LearnerUpdateById, LearnerUpdatedById> _learnerUpdateByIdHandler;

        public LearnerUpdateByIdController(
            ILearnerHandler<LearnerUpdateById, LearnerUpdatedById> learnerUpdateByIdHandler)
        {
            _learnerUpdateByIdHandler = learnerUpdateByIdHandler;
        }

        [HttpPut("{id}", Name = "UpdateLearner")]
        [ProducesResponseType(200, Type = typeof(LearnerUpdatedById))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> UpdateByIdAsync(
            [FromRoute] Guid id,
            [FromBody] LearnerUpdateById learnerUpdateById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            learnerUpdateById.Id = id;

            var learnerUpdatedById =
                await _learnerUpdateByIdHandler.ExecuteAsync(learnerUpdateById);

            return Ok(learnerUpdatedById);
        }
    }
}