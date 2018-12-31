using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerAddController : ControllerBase
    {
        private readonly ILearnerHandler<LearnerAdd, LearnerAdded> _learnerAddHandler;

        public LearnerAddController(
            ILearnerHandler<LearnerAdd, LearnerAdded> learnerAddHandler)
        {
            _learnerAddHandler = learnerAddHandler;
        }

        [HttpPost(Name = "AddLearner")]
        [ProducesResponseType(201, Type = typeof(LearnerAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> AddAsync(
            [FromBody] LearnerAdd learnerAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var learnerAdded =
                await _learnerAddHandler.ExecuteAsync(learnerAdd);

            return CreatedAtRoute("FetchLearner", new { id = learnerAdded.Id }, learnerAdded);
        }
    }
}