using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerListAdderController : ControllerBase
    {
        private readonly IHandler<LearnerListAdd, LearnerListAdded> _learnerListAdder;

        public LearnerListAdderController(
            IHandler<LearnerListAdd, LearnerListAdded> learnerListAdder)
        {
            _learnerListAdder = learnerListAdder;
        }

        [HttpPost(Name = "AddLearnerList")]
        [ProducesResponseType(201, Type = typeof(LearnerListAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> AddAsync(
            [FromBody] LearnerListAdd learnerListAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var learnerListAdded =
                await _learnerListAdder.ExecuteAsync(learnerListAdd);

            return new OkObjectResult(learnerListAdded);
        }
    }
}