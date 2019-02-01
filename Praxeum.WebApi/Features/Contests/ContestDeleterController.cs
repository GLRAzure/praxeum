using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Contests
{
    [Route("api/contests")]
    [Produces("application/json")]
    [ApiController]
    public class ContestDeleterController : ControllerBase
    {
        private readonly IHandler<ContestDelete, ContestDeleted> _contestDeleter;

        public ContestDeleterController(
            IHandler<ContestDelete, ContestDeleted> contestDeleter)
        {
            _contestDeleter = contestDeleter;
        }

        [HttpDelete("{id}", Name = "DeleteContest")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contests" })]
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] Guid id)
        {
            await _contestDeleter.ExecuteAsync(
                new ContestDelete
                {
                    Id = id
                });

            return NoContent();
        }
    }
}