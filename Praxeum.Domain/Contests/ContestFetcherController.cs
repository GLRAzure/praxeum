using System;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests
{
    [Route("api/contests")]
    [Produces("application/json")]
    [ApiController]
    public class ContestFetcherController : ControllerBase
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;

        public ContestFetcherController(
            IHandler<ContestFetch, ContestFetched> contestFetcher)
        {
            _contestFetcher = contestFetcher;
        }

        [HttpGet("{id}", Name = "FetchContest")]
        [ProducesResponseType(200, Type = typeof(ContestFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contests" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid id)
        {
            var contestFetched =
                await _contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = id
                    });

            return Ok(contestFetched);
        }
    }
}