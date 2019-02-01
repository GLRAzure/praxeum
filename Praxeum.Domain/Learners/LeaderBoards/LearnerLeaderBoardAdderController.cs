using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    [Route("api/learners/{learnerId}/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerLeaderBoardAdderController : ControllerBase
    {
        private readonly IHandler<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded> _learnerLeaderBoardAdder;

        public LearnerLeaderBoardAdderController(
            IHandler<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded> learnerLeaderBoardAdder)
        {
            _learnerLeaderBoardAdder = learnerLeaderBoardAdder;
        }

        [HttpPost("{leaderBoardId}", Name = "AddLearnerLeaderBoard")]
        [ProducesResponseType(201, Type = typeof(LearnerLeaderBoardAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learner Leader Boards" })]
        public async Task<IActionResult> AddAsync(
            [FromRoute] Guid learnerId,
            [FromRoute] Guid leaderBoardId)
        {
            await _learnerLeaderBoardAdder.ExecuteAsync(
                new LearnerLeaderBoardAdd
                {
                    LearnerId = learnerId,
                    LeaderBoardId = leaderBoardId
                });

            return new StatusCodeResult((int)HttpStatusCode.Created);
        }
    }
}