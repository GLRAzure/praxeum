using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerFetchByIdController : ControllerBase
    {
        private readonly ILearnerHandler<LearnerFetchById, LearnerFetchedById> _learnerFetchByIdHandler;

        public LearnerFetchByIdController(
            ILearnerHandler<LearnerFetchById, LearnerFetchedById> learnerFetchByIdHandler)
        {
            _learnerFetchByIdHandler = learnerFetchByIdHandler;
        }

        [HttpGet("{id}", Name = "FetchLearner")]
        [ProducesResponseType(200, Type = typeof(LearnerFetchedById))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> FetchByIdAsync(
            [FromRoute] Guid id)
        {
            var learnerFetchedById =
                await _learnerFetchByIdHandler.ExecuteAsync(
                    new LearnerFetchById
                    {
                        Id = id
                    });

            return Ok(learnerFetchedById);
        }
    }
}