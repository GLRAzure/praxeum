using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    [Route("api/learners/{learnerId}/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerLeaderBoardDeleterController : ControllerBase
    {
        private readonly IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted> _learnerLeaderBoardDeleter;

        public LearnerLeaderBoardDeleterController(
            IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted> learnerLeaderBoardDeleter)
        {
            _learnerLeaderBoardDeleter = learnerLeaderBoardDeleter;
        }

        [HttpDelete("{leaderBoardId}", Name = "DeleteLearnerLeaderBoard")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learner Leader Boards" })]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid learnerId,
            [FromRoute] Guid leaderBoardId)
        {
            var learnerLeaderBoardDelete =
                new LearnerLeaderBoardDelete
                {
                    LearnerId = learnerId,
                    LeaderBoardId = leaderBoardId
                };

            await _learnerLeaderBoardDeleter.ExecuteAsync(learnerLeaderBoardDelete);

            return NoContent();
        }
    }
}