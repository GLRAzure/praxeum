using System.Threading.Tasks;

namespace Praxeum.Domain.Contests
{
    [Route("api/contests")]
    [Produces("application/json")]
    [ApiController]
    public class ContestAdderController : ControllerBase
    {
        private readonly IHandler<ContestAdd, ContestAdded> _contestAdder;

        public ContestAdderController(
            IHandler<ContestAdd, ContestAdded> contestAdder)
        {
            _contestAdder = contestAdder;
        }

        [HttpPost(Name = "AddContest")]
        [ProducesResponseType(201, Type = typeof(ContestAdded))]
        [ProducesResponseType(400, Type = typeof(ModelStateDictionary))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contests" })]
        public async Task<IActionResult> AddAsync(
            [FromBody] ContestAdd contestAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contestAdded =
                await _contestAdder.ExecuteAsync(contestAdd);

            return CreatedAtRoute("FetchContest", new { id = contestAdded.Id }, contestAdded);
        }
    }
}