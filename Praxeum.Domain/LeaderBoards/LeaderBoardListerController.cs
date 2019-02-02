using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Domain.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardListerController : ControllerBase
    {
        private readonly IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> _leaderBoardLister;

        public LeaderBoardListerController(
            IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>> leaderBoardLister)
        {
            _leaderBoardLister = leaderBoardLister;
        }

        [HttpGet(Name = "FetchLeaderBoards")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LeaderBoardListed>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var LeaderBoardListed =
                await _leaderBoardLister.ExecuteAsync(
                    new LeaderBoardList());

            return Ok(LeaderBoardListed);
        }
    }
}