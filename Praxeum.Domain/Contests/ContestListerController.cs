using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests
{
    [Route("api/contests")]
    [Produces("application/json")]
    [ApiController]
    public class ContestListerController : ControllerBase
    {
        private readonly IHandler<ContestList, IEnumerable<ContestListed>> _contestLister;

        public ContestListerController(
            IHandler<ContestList, IEnumerable<ContestListed>> contestLister)
        {
            _contestLister = contestLister;
        }

        [HttpGet(Name = "FetchContests")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContestListed>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contests" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var ContestListed =
                await _contestLister.ExecuteAsync(
                    new ContestList());

            return Ok(ContestListed);
        }
    }
}