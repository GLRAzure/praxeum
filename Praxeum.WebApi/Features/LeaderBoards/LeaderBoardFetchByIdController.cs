using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardFetchByIdController : ControllerBase
    {
        private readonly ILeaderBoardHandler<LeaderBoardFetchById, LeaderBoardFetchedById> _leaderBoardFetchByIdHandler;

        public LeaderBoardFetchByIdController(
            ILeaderBoardHandler<LeaderBoardFetchById, LeaderBoardFetchedById> leaderBoardFetchByIdHandler)
        {
            _leaderBoardFetchByIdHandler = leaderBoardFetchByIdHandler;
        }

        [HttpGet("{id}", Name = "FetchLeaderBoard")]
        [ProducesResponseType(200, Type = typeof(LeaderBoardFetchedById))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> FetchByIdAsync(
            [FromRoute] Guid id)
        {
            var leaderBoardFetchedById =
                await _leaderBoardFetchByIdHandler.ExecuteAsync(
                    new LeaderBoardFetchById
                    {
                        Id = id
                    });

            return Ok(leaderBoardFetchedById);
        }
    }
}