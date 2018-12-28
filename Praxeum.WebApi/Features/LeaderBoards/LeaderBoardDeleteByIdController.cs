using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderBoards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardDeleteByIdController : ControllerBase
    {
        private readonly ILeaderBoardHandler<LeaderBoardDeleteById, LeaderBoardDeletedById> _leaderBoardDeleteByIdHandler;

        public LeaderBoardDeleteByIdController(
            ILeaderBoardHandler<LeaderBoardDeleteById, LeaderBoardDeletedById> leaderBoardDeleteByIdHandler)
        {
            _leaderBoardDeleteByIdHandler = leaderBoardDeleteByIdHandler;
        }

        [HttpDelete("{id}", Name = "DeleteLeaderBoard")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> DeleteByIdAsync(
           [FromRoute] Guid id)
        {
            var leaderBoardDeletedById =
                await _leaderBoardDeleteByIdHandler.ExecuteAsync(
                    new LeaderBoardDeleteById
                    {
                        Id = id
                    });

            return NoContent();
        }
    }
}