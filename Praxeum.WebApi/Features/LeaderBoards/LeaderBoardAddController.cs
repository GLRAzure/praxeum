using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardAddController : ControllerBase
    {
        private readonly ILeaderBoardHandler<LeaderBoardAdd, LeaderBoardAdded> _leaderBoardAddHandler;

        public LeaderBoardAddController(
            ILeaderBoardHandler<LeaderBoardAdd, LeaderBoardAdded> leaderBoardAddHandler)
        {
            _leaderBoardAddHandler = leaderBoardAddHandler;
        }

        [HttpPost(Name = "AddLeaderBoard")]
        [ProducesResponseType(201, Type = typeof(LeaderBoardAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> AddAsync(
            [FromBody] LeaderBoardAdd leaderBoardAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leaderBoardAdded =
                await _leaderBoardAddHandler.ExecuteAsync(leaderBoardAdd);

            return CreatedAtRoute("FetchLeaderBoard", new { id = leaderBoardAdded.Id }, leaderBoardAdded);
        }
    }
}