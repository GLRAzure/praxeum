using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardDeleterController : ControllerBase
    {
        private readonly IHandler<LeaderBoardDelete, LeaderBoardDeleted> _leaderBoardDeleter;

        public LeaderBoardDeleterController(
            IHandler<LeaderBoardDelete, LeaderBoardDeleted> leaderBoardDeleter)
        {
            _leaderBoardDeleter = leaderBoardDeleter;
        }

        [HttpDelete("{id}", Name = "DeleteLeaderBoard")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] Guid id)
        {
            var leaderBoardDeleted =
                await _leaderBoardDeleter.ExecuteAsync(
                    new LeaderBoardDelete
                    {
                        Id = id
                    });

            return NoContent();
        }
    }
}