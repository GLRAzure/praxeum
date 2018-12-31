using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerFetchListController : ControllerBase
    {
        private readonly ILearnerHandler<LearnerFetchList, IEnumerable<LearnerFetchedList>> _learnerFetchListHandler;

        public LearnerFetchListController(
            ILearnerHandler<LearnerFetchList, IEnumerable<LearnerFetchedList>> learnerFetchListHandler)
        {
            _learnerFetchListHandler = learnerFetchListHandler;
        }

        [HttpGet(Name = "FetchLearners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LearnerFetchedList>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var learnerFetched =
                await _learnerFetchListHandler.ExecuteAsync(
                    new LearnerFetchList());

            return Ok(learnerFetched);
        }
    }
}