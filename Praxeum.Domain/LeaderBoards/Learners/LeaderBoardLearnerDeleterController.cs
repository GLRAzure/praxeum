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
    public class LeaderBoardLearnerDeleterController : ControllerBase
    {
        private readonly IHandler<LeaderBoardLearnerDelete, LeaderBoardLearnerDeleted> _leaderBoardLearnerDeleter;

        public LeaderBoardLearnerDeleterController(
            IHandler<LeaderBoardLearnerDelete, LeaderBoardLearnerDeleted> leaderBoardLearnerDeleter)
        {
            _leaderBoardLearnerDeleter = leaderBoardLearnerDeleter;
        }

        [HttpDelete("{learnerId}", Name = "DeleteLeaderBoardLearner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Board Learners" })]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid leaderBoardId,
            [FromRoute] Guid learnerId)
        {
            var leaderBoardLearnerDelete =
                new LeaderBoardLearnerDelete
                {
                    LeaderBoardId = leaderBoardId,
                    LearnerId = learnerId
                };

            await _leaderBoardLearnerDeleter.ExecuteAsync(leaderBoardLearnerDelete);

            return NoContent();
        }
    }
}