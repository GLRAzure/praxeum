using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardAdderController : ControllerBase
    {
        private readonly IHandler<LeaderBoardAdd, LeaderBoardAdded> _leaderBoardAdder;

        public LeaderBoardAdderController(
            IHandler<LeaderBoardAdd, LeaderBoardAdded> leaderBoardAdder)
        {
            _leaderBoardAdder = leaderBoardAdder;
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
                await _leaderBoardAdder.ExecuteAsync(leaderBoardAdd);

            return CreatedAtRoute("FetchLeaderBoard", new { id = leaderBoardAdded.Id }, leaderBoardAdded);
        }
    }
}