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
    public class LearnerListerController : ControllerBase
    {
        private readonly IHandler<LearnerList, IEnumerable<LearnerListed>> _learnerLister;

        public LearnerListerController(
            IHandler<LearnerList, IEnumerable<LearnerListed>> learnerLister)
        {
            _learnerLister = learnerLister;
        }

        [HttpGet(Name = "FetchLearners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LearnerListed>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var learnerFetched =
                await _learnerLister.ExecuteAsync(
                    new LearnerList());

            return Ok(learnerFetched);
        }
    }
}