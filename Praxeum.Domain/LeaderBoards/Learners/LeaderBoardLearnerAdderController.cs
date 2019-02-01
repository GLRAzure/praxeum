using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    [Route("api/leaderboards/{leaderBoardId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardLearnerAdderController : ControllerBase
    {
        private readonly IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded> _leaderBoardLearnerAdder;

        public LeaderBoardLearnerAdderController(
            IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded> leaderBoardLearnerAdder)
        {
            _leaderBoardLearnerAdder = leaderBoardLearnerAdder;
        }

        [HttpPost(Name = "AddLeaderBoardLearner")]
        [ProducesResponseType(201, Type = typeof(LeaderBoardLearnerAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Board Learners" })]
        public async Task<IActionResult> AddAsync(
            [FromRoute] Guid leaderBoardId,
            [FromBody] LeaderBoardLearnerAdd leaderBoardLearnerAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            leaderBoardLearnerAdd.LeaderBoardId = 
                leaderBoardId;

            var leaderBoardLearnerAdded =
                await _leaderBoardLearnerAdder.ExecuteAsync(leaderBoardLearnerAdd);

            return CreatedAtRoute("FetchLeaderBoardLearner", new { learnerId = leaderBoardLearnerAdded.Id }, leaderBoardLearnerAdded);
        }
    }
}