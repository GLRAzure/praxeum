using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Learners
{
    [Route("api/learners/ranked")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerRankedListerController : ControllerBase
    {
        private readonly IHandler<LearnerList, IEnumerable<LearnerListed>> _learnerLister;

        public LearnerRankedListerController(
            IHandler<LearnerList, IEnumerable<LearnerListed>> learnerLister)
        {
            _learnerLister = learnerLister;
        }

        [HttpGet(Name = "FetchRankedLearners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LearnerListed>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> FetchListAsync(
            [FromQuery] int? maximumRecords = 10)
        {
            var learnerList = 
                new LearnerList
                {
                    MaximumRecords = maximumRecords
                };

            var learnerFetched =
                await _learnerLister.ExecuteAsync(
                    learnerList);

            return Ok(learnerFetched);
        }
    }
}