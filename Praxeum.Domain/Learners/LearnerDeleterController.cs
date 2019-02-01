using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerDeleterController : ControllerBase
    {
        private readonly IHandler<LearnerDelete, LearnerDeleted> _learnerDeleter;

        public LearnerDeleterController(
            IHandler<LearnerDelete, LearnerDeleted> learnerDeleter)
        {
            _learnerDeleter = learnerDeleter;
        }

        [HttpDelete("{id}", Name = "DeleteLearner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] Guid id)
        {
            var learnerDeleted =
                await _learnerDeleter.ExecuteAsync(
                    new LearnerDelete
                    {
                        Id = id
                    });

            return NoContent();
        }
    }
}