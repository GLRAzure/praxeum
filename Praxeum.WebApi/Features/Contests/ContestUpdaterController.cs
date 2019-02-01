using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Contests
{
    [Route("api/contests")]
    [Produces("application/json")]
    [ApiController]
    public class ContestUpdaterController : ControllerBase
    {
        private readonly IHandler<ContestUpdate, ContestUpdated> _contestUpdater;

        public ContestUpdaterController(
            IHandler<ContestUpdate, ContestUpdated> contestUpdater)
        {
            _contestUpdater = contestUpdater;
        }

        [HttpPut("{id}", Name = "UpdateContest")]
        [ProducesResponseType(200, Type = typeof(ContestUpdated))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contests" })]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid id,
            [FromBody] ContestUpdate contestUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contestUpdate.Id = id;

            var contestUpdated =
                await _contestUpdater.ExecuteAsync(contestUpdate);

            return Ok(contestUpdated);
        }
    }
}