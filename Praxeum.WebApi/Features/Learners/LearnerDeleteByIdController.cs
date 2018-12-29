using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerDeleteByIdController : ControllerBase
    {
        private readonly ILearnerHandler<LearnerDeleteById, LearnerDeletedById> _learnerDeleteByIdHandler;

        public LearnerDeleteByIdController(
            ILearnerHandler<LearnerDeleteById, LearnerDeletedById> learnerDeleteByIdHandler)
        {
            _learnerDeleteByIdHandler = learnerDeleteByIdHandler;
        }

        [HttpDelete("{id}", Name = "DeleteLearner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> DeleteByIdAsync(
           [FromRoute] Guid id)
        {
            var learnerDeletedById =
                await _learnerDeleteByIdHandler.ExecuteAsync(
                    new LearnerDeleteById
                    {
                        Id = id
                    });

            return NoContent();
        }
    }
}