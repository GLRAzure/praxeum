using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderBoards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardUpdateByIdController : ControllerBase
    {
        private readonly ILeaderBoardHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById> _leaderBoardUpdateByIdHandler;

        public LeaderBoardUpdateByIdController(
            ILeaderBoardHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById> leaderBoardUpdateByIdHandler)
        {
            _leaderBoardUpdateByIdHandler = leaderBoardUpdateByIdHandler;
        }

        [HttpPut("{id}", Name = "UpdateLeaderBoard")]
        [ProducesResponseType(200, Type = typeof(LeaderBoardUpdatedById))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> UpdateByIdAsync(
            [FromRoute] Guid id,
            [FromBody] LeaderBoardUpdateById leaderBoardUpdateById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            leaderBoardUpdateById.Id = id;

            var leaderBoardUpdatedById =
                await _leaderBoardUpdateByIdHandler.ExecuteAsync(leaderBoardUpdateById);

            return Ok(leaderBoardUpdatedById);
        }
    }
}