using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardUpdaterController : ControllerBase
    {
        private readonly IHandler<LeaderBoardUpdate, LeaderBoardUpdated> _leaderBoardUpdater;

        public LeaderBoardUpdaterController(
            IHandler<LeaderBoardUpdate, LeaderBoardUpdated> leaderBoardUpdater)
        {
            _leaderBoardUpdater = leaderBoardUpdater;
        }

        [HttpPut("{id}", Name = "UpdateLeaderBoard")]
        [ProducesResponseType(200, Type = typeof(LeaderBoardUpdated))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid id,
            [FromBody] LeaderBoardUpdate leaderBoardUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            leaderBoardUpdate.Id = id;

            var leaderBoardUpdated =
                await _leaderBoardUpdater.ExecuteAsync(leaderBoardUpdate);

            return Ok(leaderBoardUpdated);
        }
    }
}